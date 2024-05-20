/// Copyright (c) 1998-$Date: 2013-12-20 07:48:17 -0800 (Fri, 20 Dec 2013) $ TIBCO Software Inc.
/// All rights reserved.
/// TIB/Rendezvous is protected under US Patent No. 5,187,787.
/// For more information, please contact:
/// TIBCO Software Inc., Palo Alto, California, USA
using System;
using System.Threading;
using TIBCO.Rendezvous;

namespace TIBCO.Rendezvous.Examples
{
	/// <summary>
	/// RendezvousCMSender - sends certified messages on specified subject.
	/// 
	/// This example publishes certified messages on the specified subject
	/// and then quits.
	/// 
	/// You can use this example with RendezvousCMListener or with
	/// RendezvousCMQueueWorker examples to see Distributed Queue
	/// in action.
	/// 
	/// There are no required parameters for this example.
	/// Optional parameters are:
	/// 
	/// -service   - RVD transport parameter
	/// -network   - RVD transport parameter
	/// -daemon    - RVD transport parameter
	/// -cmname    - CM name used by CM transport
	/// -subject   - subject this example sends messages on
	/// -count     - how many messages to send
	/// 
	/// If no transport parameters are specified, default values are used.
	/// For information on default values for these parameters,  please see
	/// the TIBCO/Rendezvous Concepts manual.
	/// 
	/// Default values for other parameters:
	/// cmname      "cm.sender.cmname"
	/// subject     "cm.test.subject"
	/// count       10
	/// 
	/// </summary>
	public class CMSenderApplication
	{
		// RVD transport parameters
		static string service = null;
		static string network = null;
		static string daemon = null;

		// Subject we use to listen messages on
		static string subject = "cm.test.subject";
    
		// Our unique CM name
		static string cmname = "cm.sender.cmname";

		// Ledger file name
		static string ledgerFilename = null;
		
		// Count of messages to be sent
		static int count = 10;

		// Confirmation advisory subject
		static String confirmAdvisorySubject = "_RV.INFO.RVCM.DELIVERY.CONFIRM.>";

		// seqno of the last message, 0 is invalid value
		static ulong lastSeqno = 0;

		// Used to lock the last seqno
		static Object lockSeqno = new Object();

		static Listener confirmListener = null;
		static Dispatcher dispatcher = null;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[MTAThread]
		static void Main(string[] arguments)
		{
			InitializeParameters(arguments);

			try
			{
				TIBCO.Rendezvous.Environment.Open();
			}
			catch(RendezvousException exception)
			{
				Console.Error.WriteLine("Failed to open Rendezvous Environment: " + exception.Message);
				Console.Error.WriteLine(exception.StackTrace);
				System.Environment.Exit(1);
			}

			// Create Network transport
			NetTransport netTransport = null;
			try
			{
				netTransport = new NetTransport(service, network, daemon);
			}
			catch (RendezvousException exception)
			{
				Console.Error.WriteLine("Failed to create NetTransport: " + exception.Message);
				Console.Error.WriteLine(exception.StackTrace);
				System.Environment.Exit(1);
			}

			// Create CM transport
			CMTransport cmTransport = null;
			try
			{
				cmTransport = new CMTransport(netTransport, cmname, true, ledgerFilename, false);

			}
			catch (RendezvousException exception)
			{
				Console.Error.WriteLine("Failed to create CMTransport: " + exception.Message);
				Console.Error.WriteLine(exception.StackTrace);
				System.Environment.Exit(1);
			}

			// Create listener for delivery confirmation
			// advisory messages
			try
			{
				confirmListener = new Listener(Queue.Default, netTransport, confirmAdvisorySubject, null);
				confirmListener.MessageReceived += new MessageReceivedEventHandler(OnMessageConfirmed);
			}
			catch (RendezvousException exception)
			{
				Console.Error.WriteLine("Failed to create CMListener: " + exception.Message);
				Console.Error.WriteLine(exception.StackTrace);
				System.Environment.Exit(1);
			}

	        // Dispatch default queue
			dispatcher = new Dispatcher(Queue.Default);

			Console.Out.WriteLine("Publishing {0} certified messages on subject {1}", count, subject);

			// Create the message
			CMMessage cmMessage = new CMMessage();

			try
			{
				cmMessage.SendSubject = subject;
				cmMessage.TimeLimit = 5.0;
				// Publish count messages.
				for (int i=1; i <= count; i++)
				{
					// Delay for 1 second
					Thread.Sleep(1000);

					// Distinguish sent messages
					cmMessage.UpdateField("index", i);

					Console.Out.WriteLine("Publishing message {0}", cmMessage);
					Console.Out.Flush();

					// we must block access to lastSeqno because
					// the confirmation may arrive before we can
					// retrieve seqno from the message we just sent

					lock(lockSeqno)
					{
						// Send message into the queue
						cmTransport.Send(cmMessage);

						// If it was the last message, get it's seqno
						if (i == count)
						{
							lastSeqno = cmMessage.SequenceNumber;
							Console.Out.WriteLine("Last sequence number to be confirmed {0}", lastSeqno);
							Console.Out.Flush();
						}
					}
				}

				// wait until the last message has been confirmed
				dispatcher.Join();
			}
			catch(RendezvousException exception)
			{
				Console.Error.WriteLine("Error while sending messages: {0}", exception.Message);
				Console.Error.WriteLine(exception.StackTrace);
				System.Environment.Exit(1);
			}
			
			TIBCO.Rendezvous.Environment.Close();
		}

		static void OnMessageConfirmed(object listener, MessageReceivedEventArgs messageReceivedEventArgs)
		{
			ulong seqno = 0;
			Message message = messageReceivedEventArgs.Message;

			// because we check if the last message was confirmed we
			// should synchronize access to it

			lock(lockSeqno)
			{
				try
				{
					seqno = message.GetField("seqno", 0);
					Console.Out.WriteLine("Confirmed message with seqno={0}", seqno);
					Console.Out.Flush();
				}
				catch(RendezvousException exception)
				{
					Console.Error.WriteLine("Exception occurred while getting \"seqno\" field from DELIVERY.CONFIRM advisory message:");
					Console.Error.WriteLine(exception.StackTrace);
					System.Environment.Exit(1);
				}
				try
				{
					// check if the last message has been confirmed
					// and if it was the last message, close Tibrv
					if (lastSeqno > 0 && lastSeqno == seqno)
						dispatcher.Destroy();
				}
				catch(RendezvousException exception)
				{
					Console.Error.WriteLine("Exception occurred in Environment.Close():");
					Console.Error.WriteLine(exception.StackTrace);
					System.Environment.Exit(1);
				}
			}
		}

		static void Usage()
		{
			Console.Out.WriteLine("Usage: RendezvousCMSender [-service service] [-network network]");
			Console.Out.WriteLine("                          [-daemon daemon] [-cmname cmname]");
			Console.Out.WriteLine("                          [-subject subject] [-count NNN]");
			Console.Out.WriteLine("                          [-ledger filename] ");
			Console.Out.WriteLine("    default values are:");
			Console.Out.WriteLine("       service = " + service);
			Console.Out.WriteLine("       network = " + network);
			Console.Out.WriteLine("       daemon  = " + daemon);
			Console.Out.WriteLine("       cmname  = " + cmname);
			Console.Out.WriteLine("       subject = " + subject);
			Console.Out.WriteLine("       count   = " + count);
			System.Environment.Exit(1);
		}

		static int InitializeParameters(string[] arguments)
		{
			int i = 0;
			while(i < arguments.Length - 1 && arguments[i].StartsWith("-"))
			{
				if (arguments[i].Equals("-service"))
				{
					service = arguments[i+1];
					i += 2;
				}
				else if (arguments[i].Equals("-network"))
				{
					network = arguments[i+1];
					i += 2;
				}
				else if (arguments[i].Equals("-daemon"))
				{
					daemon = arguments[i+1];
					i += 2;
				}
				else if (arguments[i].Equals("-subject"))
				{
					subject = arguments[i+1];
					i += 2;
				}
				else if (arguments[i].Equals("-cmname"))
				{
					cmname = arguments[i+1];
					i += 2;
				}
				else if (arguments[i].Equals("-ledger"))
				{
					ledgerFilename = arguments[i + 1];
					i += 2;
				}
				else
					Usage();
			}
			return i;
		}
	}
}

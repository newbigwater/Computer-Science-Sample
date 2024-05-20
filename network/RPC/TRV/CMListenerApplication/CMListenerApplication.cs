/// Copyright (c) 1998-$Date: 2013-12-20 07:48:17 -0800 (Fri, 20 Dec 2013) $ TIBCO Software Inc.
/// All rights reserved.
/// TIB/Rendezvous is protected under US Patent No. 5,187,787.
/// For more information, please contact:
/// TIBCO Software Inc., Palo Alto, California, USA

using System;
using TIBCO.Rendezvous;

namespace TIBCO.Rendezvous.Examples
{
	/// <summary>
	/// RendezvousCMListener - listens for certified messages and confirms them
	/// 
	/// There are no required parameters for this example.
	/// Optional parameters are: 
	/// 
	/// -service   - RVD transport parameter
	/// -network   - RVD transport parameter
	/// -daemon    - RVD transport parameter
	/// -cmname    - CM name used by CM transport
	/// -subject   - subject this example listens on
	/// </summary>
	public class CMListenerApplication
	{
		// RVD transport parameters
		static string service = null;
		static string network = null;
		static string daemon = null;

		// Subject we use to listen messages on
		static string subject = "cm.test.subject";
    
		// Our unique CM name
		static string cmname = "cm.listener.cmname";

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[MTAThread]
		static void main(string[] arguments)
		{
			InitializeParameters(arguments);

			try
			{
				TIBCO.Rendezvous.Environment.Open();
			}
			catch(RendezvousException exception)
			{
				Console.Error.WriteLine("Failed to open Rendezvous Environment: {0}", exception.Message);
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
				Console.Error.WriteLine("Failed to create NetTransport: {0}", exception.Message);
				Console.Error.WriteLine(exception.StackTrace);
				System.Environment.Exit(1);
			}

			// Create CM transport
			CMTransport cmTransport = null;
			try
			{
				cmTransport = new CMTransport(netTransport, cmname, true);

			}
			catch (RendezvousException exception)
			{
				Console.Error.WriteLine("Failed to create CMTransport: {0}", exception.Message);
				Console.Error.WriteLine(exception.StackTrace);
				System.Environment.Exit(1);
			}

			// Create Queue
			Queue queue = null;
			try
			{
				queue = new Queue();
			}
			catch (RendezvousException exception)
			{
				Console.Error.WriteLine("Failed to create Queue: {0}", exception.Message);
				Console.Error.WriteLine(exception.StackTrace);
				System.Environment.Exit(1);
			}

			// Create listener for CM messages
			CMListener cmListener = null;
			try
			{
				cmListener = new CMListener(queue,cmTransport, subject, null);
				cmListener.MessageReceived += new MessageReceivedEventHandler(OnMessageReceived);
				// Set explicit confirmation
				cmListener.SetExplicitConfirmation();
			}
			catch (RendezvousException exception)
			{
				Console.Error.WriteLine("Failed to create CMListener: {0}", exception.Message);
				Console.Error.WriteLine(exception.StackTrace);
				System.Environment.Exit(1);
			}

			// Report we are running Ok
			Console.Out.WriteLine("Listening on subject: {0}", subject);

			// dispatch Rendezvous events
			try
			{
				Dispatcher dispatcher = new Dispatcher(queue);
				dispatcher.Join();
			}
			catch(Exception exception)
			{
				Console.Error.WriteLine("Exception dispatching default queue: {0}", exception.Message);					
				Console.Error.WriteLine(exception.StackTrace);
				System.Environment.Exit(1);
			}
		}
		
		static void OnMessageReceived(object listener, MessageReceivedEventArgs messageReceivedEventArgs)
		{
			Message message = messageReceivedEventArgs.Message;
			Console.Out.WriteLine("Received message: {0}", message);

			try
			{
				if (message is CMMessage)
				{
					CMMessage cmMessage = (CMMessage) message ;
					
					ulong seqno = 0;

					try
					{
						seqno = cmMessage.SequenceNumber;
					}
					catch (RendezvousException exception)
					{
						if (exception.Status != Status.NotFound)
						{
							Console.Out.WriteLine("Failed to retrieve CM message sequence number: {0}", exception.Message);
							Console.Out.WriteLine(exception.StackTrace);
							System.Environment.Exit(1);
						}
					}

					// If it was not CM message or very first message
					// we'll get seqno=0. Only confirm if seqno > 0.
					if (seqno > 0)
					{
						Console.Out.WriteLine("Confirming message with seqno={0}", seqno);
						Console.Out.Flush();

						// Confirm the message						
						((CMListener) listener).ConfirmMessage(cmMessage);
					}
				}
			}
			catch (Exception exception)
			{
				Console.Out.WriteLine("Failed to confirm CM message: {0}", exception.Message);
			}

			// if message had the reply subject, send the reply
			try
			{
				if (message.ReplySubject != null)
				{
					Message reply = new Message(message);
					((CMListener) listener).Transport.SendReply(reply, message);
				}
			}
			catch (Exception exception)
			{
				Console.Out.WriteLine("Failed to send reply: {0}", exception.Message);
				Console.Out.WriteLine(exception.StackTrace);
			}
		}

		static void Usage()
		{
			Console.Out.Write("Usage: RendezvousCMListener [-service service] [-network network]");
			Console.Out.Write("                            [-daemon daemon] [-cmname cmname]");
			Console.Out.Write("                            [-subject subject]");
			Console.Out.Write("    default values are:");
			Console.Out.Write("       service = " + service);
			Console.Out.Write("       network = " + network);
			Console.Out.Write("       daemon  = " + daemon);
			Console.Out.Write("       cmname  = " + cmname);
			Console.Out.Write("       subject = " + subject);			
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
				else
					Usage();
			}
			return i;
		}
	}
}

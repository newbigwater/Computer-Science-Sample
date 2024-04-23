using System;
using System.Activities;

public sealed class ReadIntCheck : NativeActivity<int>
{
    private InArgument<string> bookmarkName;

    [RequiredArgument]
    public InArgument<string> BookmarkName
    {
        get {  return bookmarkName; }
        set
        {
            bookmarkName = value;
        }
    }

    protected override void Execute(NativeActivityContext context)
    {
        string name = BookmarkName.Get(context);

        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("BookmarkName cannot be an Empty string.", "context");
        }

        context.CreateBookmark(name, new BookmarkCallback(OnReadComplete));
    }

    // NativeActivity derived activities that do asynchronous operations by calling
    // one of the CreateBookmark overloads defined on System.Activities.NativeActivityContext
    // must override the CanInduceIdle property and return true.
    protected override bool CanInduceIdle
    {
        get { return true; }
    }

    public void OnReadComplete(NativeActivityContext context, Bookmark bookmark, object state)
    {
        this.Result.Set(context, Convert.ToInt32(state));
    }
}
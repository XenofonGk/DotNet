
class TodoItem
{
    public string Title { get; private set; }
    public bool IsDone { get; private set; }

    public TodoItem ( string title)
    {
        Title = title;
        IsDone = false;
    }

    public void MarkDone()
    {
        IsDone = true;
    }


}
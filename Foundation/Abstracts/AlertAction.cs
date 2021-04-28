namespace Foundation.Abstracts
{
    public class AlertAction
    {
        public string Text { get; }
        public System.Action ActionButton { get; }

        public AlertAction(string text)
        {
            Text = text;
        }

        public AlertAction(string text, System.Action action) : this(text)
        {
            ActionButton = action;
        }
    }
}
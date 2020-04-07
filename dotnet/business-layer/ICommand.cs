namespace dk.opusmagus.fd.bl
{
    public interface ICommand<I, O>
    {
        O Execute(I input);
    }
}
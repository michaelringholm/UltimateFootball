namespace dk.commentor.sl
{
    public interface ISecurityVault {
        string GetKey(string keyName);
        string GetSecret(string secretName);
        void CreateSecret(string secretName, string secret);
    }
}
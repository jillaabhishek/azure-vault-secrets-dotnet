using System;
using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace dotnetcore
{
    class Program
    {
        static void Main(string[] args)
        {
            string secretName = "MySecret";
            string keyVaultName = "my-vault-no-protect94";
            var kvUri = "https://my-vault-no-protect94.vault.azure.net/";

            SecretClientOptions options = new SecretClientOptions(){
                Retry = {
                    Delay = TimeSpan.FromSeconds(2),
                    MaxDelay = TimeSpan.FromSeconds(16),
                    MaxRetries = 5,
                    Mode = RetryMode.Exponential
                }
            };

            var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential(), options);
            KeyVaultSecret secret = client.GetSecret(secretName);

            System.Console.WriteLine("Get Secret: " + secret.Value);
            System.Console.Write("Enter Secret >");

            string secretValue = Console.ReadLine();
            client.SetSecret(secretName, secretValue);
            System.Console.Write("  SetSecret:" );
            System.Console.Write(" Key:" + secretName);
            System.Console.Write("  Value: " + secretValue);

            System.Console.WriteLine("GetSecret:" + secret.Value);

            client.StartDeleteSecret(secretName);
            System.Console.WriteLine("StartDeleteSecret:" + keyVaultName);

            System.Console.WriteLine("GetSecret: "+secret.Value);




        }
    }
}

using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using laundry.project.Entities;

namespace laundry.project.Infrastructure
{
    public class AzureSender : ISender
    {
        // Connexion à Azure IoT Hub avec la chaîne de connexion 
        private static readonly string connectionString = "HostName=IotHubTp3.azure-devices.net;DeviceId=Client1;SharedAccessKey=193BvsEeFSSRATul+VYqNxJ9rpYcpDedDaabHYJvMPg=";

        // Création du DeviceClient pour communiquer avec Azure IoT Hub
        private static readonly DeviceClient deviceClient = DeviceClient.CreateFromConnectionString(connectionString, TransportType.Mqtt);

        // Méthode pour envoyer un message asynchrone à Azure IoT Hub
        public async Task SendMessageAsync(Entities.Message message)
        {
            // Vérification des données pour s'assurer que tout est bien formaté
            if (message == null)
            {
                Console.WriteLine("Le message est null !");
                return;
            }

            // Construction de la structure de data
            var data = new
            {
                
                machineId = message.IdMachine, // ID de la machine
                state = message.State.ToString(), // État de la machinE
                timestamp = message.Date.ToString("yyyy-MM-dd HH:mm:ss") //  Format du data
            };

            // Sérialisation de la structure en JSON
            string jsonPayload = JsonSerializer.Serialize(data);

            // Création du message à envoyer à Azure IoT Hub
            var azureMessage = new Microsoft.Azure.Devices.Client.Message(Encoding.UTF8.GetBytes(jsonPayload));

            // Ajout d'une propriété à la message
            azureMessage.Properties.Add("machineState", message.State.ToString());

            // Envoi du message à Azure
            await deviceClient.SendEventAsync(azureMessage);

            // Affichage du message envoyé
            Console.WriteLine($"[AzureIoTHub] Message envoyé : {jsonPayload}");
        }

        // Méthode synchrone pour envoyer un message
        public void SendMessage(Entities.Message message)
        {
            // Appel de la méthode asynchrone
            SendMessageAsync(message).GetAwaiter().GetResult();
        }
    }
}

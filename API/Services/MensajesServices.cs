using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using API.Models;

namespace API.Services
{
    public class MensajesServices
    {
        private readonly IMongoCollection<Chat> _Mensajes;
        public MensajesServices(IUsuariosDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _Mensajes = database.GetCollection<Chat>(settings.UsuariosCollectionName);
        }

        public List<Chat> Get() =>
            _Mensajes.Find(mensaje => true).ToList();

        public Chat Get(string id) =>
            _Mensajes.Find<Chat>(mensaje => mensaje.id == id).FirstOrDefault();
        
        public Chat Create(Chat mensaje)
        {
            _Mensajes.InsertOne(mensaje);
            return mensaje;
        }
        public void Update(string id, Chat mensajeIn) =>
            _Mensajes.ReplaceOne(mensaje => mensaje.id == id, mensajeIn);

        public void Remove(Chat mensajeIn) =>
            _Mensajes.DeleteOne(mensaje => mensaje.id == mensajeIn.id);

        public void Remove(string id) =>
            _Mensajes.DeleteOne(mensaje => mensaje.id == id);

    }
}

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
        private readonly IMongoCollection<Mensajes> _Mensajes;
        public MensajesServices(IMensajesDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _Mensajes = database.GetCollection<Mensajes>(settings.MensajesCollectionName);
        }

        public List<Mensajes> Get() =>
            _Mensajes.Find(mensaje => true).ToList();

        public Mensajes Get(string id) =>
            _Mensajes.Find<Mensajes>(mensaje => mensaje.Id == id).FirstOrDefault();
        
        public Mensajes Create(Mensajes mensaje)
        {
            _Mensajes.InsertOne(mensaje);
            return mensaje;
        }
        public void Update(string id, Mensajes mensajeIn) =>
            _Mensajes.ReplaceOne(mensaje => mensaje.Id == id, mensajeIn);

        public void Remove(Mensajes mensajeIn) =>
            _Mensajes.DeleteOne(mensaje => mensaje.Id == mensajeIn.Id);

        public void Remove(string id) =>
            _Mensajes.DeleteOne(mensaje => mensaje.Id == id);

    }
}

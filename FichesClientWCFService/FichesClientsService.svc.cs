using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace FichesClientWCFService
{
    public class FichesClientsService : IFichesClientService
    {
        public bool AjoutFicheClient()
        {
            try
            {
                using (var entities = new FichesClientsEntities())
                {
                    entities.Clients.Add(new Clients()
                    {
                        Nom = "Ajout",
                        Prenom = "Ajout",
                        Age = 0,
                        Sexe = "M"
                    });

                    entities.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public ObservableCollection<FicheClient> GetFiches()
        {
            ObservableCollection<FicheClient> collec = null;

            using (var entities = new FichesClientsEntities())
            {
                collec = new ObservableCollection<FicheClient>(entities.Clients.Select(
                    client => new FicheClient()
                    {
                        Nom = client.Nom,
                        Prenom = client.Prenom,
                        Age = client.Age.Value,
                        Sexe = client.Sexe
                    }
                    ));
            }

            return collec;
        }

        public ObservableCollection<FicheClient> GetFichesClientsMasculins()
        {
            ObservableCollection<FicheClient> collec = null;

            using (var entities = new FichesClientsEntities())
            {
                collec = new ObservableCollection<FicheClient>(entities.Clients.Where(c => c.Sexe == "M").Select(
                    client => new FicheClient()
                    {
                        Nom = client.Nom,
                        Prenom = client.Prenom,
                        Age = client.Age.Value,
                        Sexe = client.Sexe
                    }
                    ));
            }

            return collec;
        }

        public bool ModificationFicheClient(FicheClient fiche)
        {
            try
            {
                using (var entities = new FichesClientsEntities())
                {
                    var ficheAModifier = entities.Clients.FirstOrDefault((client) =>
                         client.Nom == fiche.Nom && client.Prenom == fiche.Prenom);

                    if (ficheAModifier != null)
                    {
                        ficheAModifier.Sexe = fiche.Sexe;
                        ficheAModifier.Age = fiche.Age;

                        entities.SaveChanges();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SuppressionFicheClient(FicheClient fiche)
        {
            try
            {
                using (var entities = new FichesClientsEntities())
                {
                    var fichesASupprimer = entities.Clients.Where((client) =>
                         client.Nom == fiche.Nom && client.Prenom == fiche.Prenom && client.Age == fiche.Age && client.Sexe == fiche.Sexe).ToArray();

                    entities.Clients.RemoveRange(fichesASupprimer);

                    entities.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace FichesClientWCFService
{
    [ServiceContract]
    public interface IFichesClientService
    {
        [OperationContract]
        ObservableCollection<FicheClient> GetFiches();

        [OperationContract]
        ObservableCollection<FicheClient> GetFichesClientsMasculins();

        [OperationContract]
        bool AjoutFicheClient();

        [OperationContract]
        bool SuppressionFicheClient(FicheClient fiche);

        [OperationContract]
        bool ModificationFicheClient(FicheClient fiche);

    }

    [DataContract]
    public class FicheClient : INotifyPropertyChanged
    {
        string nom;

        string prenom;

        string sexe;

        int age;

        public event PropertyChangedEventHandler PropertyChanged;

        [DataMember]
        public string Nom
        {
            get
            {
                return nom;
            }

            set
            {
                if (nom != value)
                {
                    nom = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DataMember]
        public string Prenom
        {
            get
            {
                return prenom;
            }

            set
            {
                if (prenom != value)
                {
                    prenom = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DataMember]
        public string Sexe
        {
            get
            {
                return sexe;
            }

            set
            {
                if (sexe != value)
                {
                    sexe = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [DataMember]
        public int Age
        {
            get
            {
                return age;
            }

            set
            {
                if (age != value)
                {
                    age = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public void NotifyPropertyChanged([CallerMemberName] string str = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(str));
        }
    }
}

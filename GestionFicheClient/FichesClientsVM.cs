using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionFichesClients.FichesClientsService;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace GestionFichesClients
{
    class FichesClientsVM : INotifyPropertyChanged
    {
        private FichesClientsService.FicheClient ficheSelectionnee;

        private ObservableCollection<FichesClientsService.FicheClient> listeDeFichesClients;

        public ObservableCollection<FicheClient> ListeDeFichesClients
        {
            get
            {
                return listeDeFichesClients;
            }

            private set
            {
                if (listeDeFichesClients != value)
                {
                    listeDeFichesClients = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public FicheClient FicheSelectionnee
        {
            get
            {
                return ficheSelectionnee;
            }

            set
            {
                if (ficheSelectionnee != value)
                {
                    ficheSelectionnee = value;
                    NotifyPropertyChanged();
                }
            }
        }

        ICommand ajouterFicheClient;
        public ICommand AjouterFicheClient
        {
            get
            {
                if (ajouterFicheClient == null)
                {
                    ajouterFicheClient = new RelayCommand<object>((obj) =>
                    {
                        var client = new FichesClientsService.FichesClientServiceClient();
                        client.AjoutFicheClient();
                        ListeDeFichesClients = new ObservableCollection<FicheClient>(client.GetFiches());
                        client.Close();
                    });
                }
                return ajouterFicheClient;
            }
        }

        ICommand supprimerFicheClient;
        public ICommand SupprimerFicheClient
        {
            get
            {
                if (supprimerFicheClient == null)
                {
                    supprimerFicheClient = new RelayCommand<FicheClient>((fiche) =>
                    {
                        var client = new FichesClientsService.FichesClientServiceClient();
                        client.SuppressionFicheClient(fiche);
                        ListeDeFichesClients = new ObservableCollection<FicheClient>(client.GetFiches());
                        client.Close();
                    });
                }
                return supprimerFicheClient;
            }
        }

        ICommand modifierFicheClient;
        public ICommand ModifierFicheClient
        {
            get
            {
                if (modifierFicheClient == null)
                {
                    modifierFicheClient = new RelayCommand<FicheClient>((fiche) =>
                    {
                        var client = new FichesClientsService.FichesClientServiceClient();
                        client.ModificationFicheClient(fiche);
                        ListeDeFichesClients = new ObservableCollection<FicheClient>(client.GetFiches());
                        client.Close();
                    });
                }
                return modifierFicheClient;
            }
        }

        ICommand editerFicheClient;
        public ICommand EditerFicheClient
        {
            get
            {
                if (editerFicheClient == null)
                {
                    editerFicheClient = new RelayCommand<FicheClient>((fiche) =>
                    {
                        FicheSelectionnee = fiche;
                    });
                }
                return editerFicheClient;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string str = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(str));
        }

        public FichesClientsVM()
        {
            var client = new FichesClientsService.FichesClientServiceClient();
            ListeDeFichesClients = new ObservableCollection<FicheClient>(client.GetFiches());
            client.Close();
        }
    }
}

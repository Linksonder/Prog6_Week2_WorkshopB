using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MusicCollectionMVVMLight.Model;
using MusicCollectionMVVMMVVMLight.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;

namespace MusicCollectionMVVMLight.ViewModel
{
    public class SongListViewModel : ViewModelBase
    {
        ISongRepository songRepository;

        public ObservableCollection<SongViewModel> Songs { get; set; }

        public SongViewModel SongViewModel { get; set; }

        public ICommand AddSong { get; set; }

        public SongListViewModel()
        {
            songRepository = new DummySongRepository();
            var songList = songRepository.ToList().Select(s => new SongViewModel(s));


            AddSong = new RelayCommand(AddNewSong, CanAddNewSong);
            SongViewModel = new SongViewModel();
            Songs = new ObservableCollection<SongViewModel>(songList);
        }

        private void AddNewSong()
        {
            var svm = new SongViewModel();

            svm.Artist = SongViewModel.Artist;
            svm.Title = SongViewModel.Title;
            svm.Id = SongViewModel.Id;

            Songs.Add(svm);
        }

        private bool CanAddNewSong()
        {
            if (SongViewModel == null)
                return false;

            if (SongViewModel.Id <= 0)
                return false;

            if (String.IsNullOrEmpty(SongViewModel.Artist) || String.IsNullOrEmpty(SongViewModel.Title))
                return false;

            return true;
        }



    }
}
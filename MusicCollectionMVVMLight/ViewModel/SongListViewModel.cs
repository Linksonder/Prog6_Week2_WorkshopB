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

        public SongListViewModel()
        {
            songRepository = new DummySongRepository();
            var songList = songRepository.ToList().Select(s => new SongViewModel(s));


            AddSong = new RelayCommand(AddNewSong, CanAddNewSong);
            Song = new SongViewModel();
            Songs = new ObservableCollection<SongViewModel>(songList);
        }

        private void AddNewSong()
        {
            Songs.Add(Song);
        }

        private bool CanAddNewSong()
        {
            if (Song == null)
                return false;

            if (Song.Id <= 0)
                return false;

            if (String.IsNullOrEmpty(Song.Artist) || String.IsNullOrEmpty(Song.Title))
                return false;

            return true;
        }


        public ObservableCollection<SongViewModel> Songs { get; set; }

        public SongViewModel Song { get; set; }

        public ICommand AddSong { get; set; }
    }
}
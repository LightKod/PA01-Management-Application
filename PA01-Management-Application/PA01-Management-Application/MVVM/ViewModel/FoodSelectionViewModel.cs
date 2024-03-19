using Microsoft.VisualBasic;
using PA01_Management_Application.Core;
using PA01_Management_Application.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PA01_Management_Application.MVVM.ViewModel
{
    class FoodSelectionViewModel : BaseViewModel
    {
        ObservableCollection<KeyValuePair<Food, int>> foodAmountCollection;
        List<Food> foodList;

        static Room room;
        public ObservableCollection<KeyValuePair<Food, int>> FoodAmountCollection
        {
            get { return foodAmountCollection; }
            set 
            {
                foodAmountCollection = value;
                OnPropertyChanged(nameof(FoodAmountCollection));
            }
        }

        public RelayCommand SubFoodCommand { get; }
        public RelayCommand AddFoodCommand { get; }
        public RelayCommand ComfirmCommand { get; }

        public FoodSelectionViewModel()
        {
            CreateSampleFood();
            AddFoodCommand = new(AddFoodCommandExecute);
            SubFoodCommand = new(SubFoodCommandExecute);
            ComfirmCommand = new(ComfirmCommandExecute);
        }

        private void ComfirmCommandExecute(object obj)
        {
            (Application.Current.MainWindow.DataContext as AppWindowViewModel).CurrentView = new PaymentPreviewViewModel();
        }

        public FoodSelectionViewModel(Room _room, Room displayRoom) : base()
        {
            room = _room;
           
        }

        private void SubFoodCommandExecute(object parameter)
        {
            Food food = foodList.Find(x => x.ID == parameter);
            if (food == null) return;
            int index = foodAmountCollection.IndexOf(foodAmountCollection.First(x => x.Key.ID == parameter));


            if (foodAmountCollection[index].Value <= 0)
            {
                return;
            }
            foodAmountCollection[index] = new(foodAmountCollection[index].Key, foodAmountCollection[index].Value - 1);
            OnPropertyChanged(nameof(FoodAmountCollection));
        }

        private void AddFoodCommandExecute(object parameter)
        {
            Food food = foodList.Find(x => x.ID == parameter);
            if (food == null) return;
            int index = foodAmountCollection.IndexOf(foodAmountCollection.First(x => x.Key.ID == parameter));


            foodAmountCollection[index] = new(foodAmountCollection[index].Key, foodAmountCollection[index].Value + 1);
            OnPropertyChanged(nameof(FoodAmountCollection));
        }


        void CreateSampleFood()
        {
            foodList = new();
            foodAmountCollection = new();

            foodList.Add(new("FCB1", "Combo 1", "1 Drink + 2 Popcorn", 20, "https://iguov8nhvyobj.vcdn.cloud/media/wysiwyg/2022/082022/Birthday_Popcorn_Box_350x495.png"));
            foodList.Add(new("FCB2", "Combo 1", "2 Drink + 2 Popcorn", 40, "https://iguov8nhvyobj.vcdn.cloud/media/wysiwyg/2022/082022/Birthday_Popcorn_Box_350x495.png"));
            foodList.Add(new("FCB3", "Combo 1", "2 Drink + 2 Popcorn", 40, "https://iguov8nhvyobj.vcdn.cloud/media/wysiwyg/2022/082022/Birthday_Popcorn_Box_350x495.png"));
            foodList.Add(new("FCB4", "Combo 1", "2 Drink + 2 Popcorn", 40, "https://iguov8nhvyobj.vcdn.cloud/media/wysiwyg/2022/082022/Birthday_Popcorn_Box_350x495.png"));
            foodList.Add(new("FCB5", "Combo 1", "2 Drink + 2 Popcorn", 40, "https://iguov8nhvyobj.vcdn.cloud/media/wysiwyg/2022/082022/Birthday_Popcorn_Box_350x495.png"));
            foodList.Add(new("FCB6", "Combo 1", "2 Drink + 2 Popcorn", 40, "https://iguov8nhvyobj.vcdn.cloud/media/wysiwyg/2022/082022/Birthday_Popcorn_Box_350x495.png"));

            foreach (var food in foodList)
            {
                foodAmountCollection.Add(new(food, 0));
            }
        }
    }
}

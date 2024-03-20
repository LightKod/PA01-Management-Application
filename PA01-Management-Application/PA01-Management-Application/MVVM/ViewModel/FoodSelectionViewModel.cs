using Microsoft.VisualBasic;
using PA01_Management_Application.Core;
using PA01_Management_Application.DataManagers;
using PA01_Management_Application.MVVM.Models;
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
            BookingDataHolder.foods = foodAmountCollection;
            (Application.Current.MainWindow.DataContext as AppWindowViewModel).CurrentView = new PaymentPreviewViewModel();
        }


        private void SubFoodCommandExecute(object parameter)
        {
            if(parameter is int id)
            {
                Food food = foodList.Find(x => x.FoodId == id);
                if (food == null) return;
                int index = foodAmountCollection.IndexOf(foodAmountCollection.First(x => x.Key.FoodId == id));


                if (foodAmountCollection[index].Value <= 0)
                {
                    return;
                }
                foodAmountCollection[index] = new(foodAmountCollection[index].Key, foodAmountCollection[index].Value - 1);
                OnPropertyChanged(nameof(FoodAmountCollection));
            }
            
        }

        private void AddFoodCommandExecute(object parameter)
        {
            if (parameter is int id)
            {
                Food food = foodList.Find(x => x.FoodId == id);
                if (food == null) return;
                int index = foodAmountCollection.IndexOf(foodAmountCollection.First(x => x.Key.FoodId == id));


                foodAmountCollection[index] = new(foodAmountCollection[index].Key, foodAmountCollection[index].Value + 1);
                OnPropertyChanged(nameof(FoodAmountCollection));
            }
        }


        async void CreateSampleFood()
        {
            foodList = new();
            foodAmountCollection = new();

            MovieManagementContext context = new MovieManagementContext();
            foodList = context.Foods.ToList();
       

            foreach (var food in foodList)
            {
                foodAmountCollection.Add(new(food, 0));
            }
        }
    }
}

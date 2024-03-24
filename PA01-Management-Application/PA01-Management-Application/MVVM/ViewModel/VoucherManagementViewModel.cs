using PA01_Management_Application.Core;
using PA01_Management_Application.MVVM.Models;
using PA01_Management_Application.MVVM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PA01_Management_Application.MVVM.ViewModel
{
    internal class VoucherManagementViewModel : ObservableObject
    {
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand AddCommand { get; set; }

        private ObservableCollection<Voucher> _voucherList;
        public ObservableCollection<Voucher> VoucherList
        {
            get { return _voucherList; }
            set
            {
                _voucherList = value;
                OnPropertyChanged(nameof(VoucherList));
            }
        }

        public VoucherManagementViewModel()
        {

            // Khởi tạo danh sách voucher và các lệnh
            RefreshVoucherList();

            // Khởi tạo các lệnh
            DeleteCommand = new RelayCommand(DeleteVoucher);
            AddCommand = new RelayCommand(AddVoucher);
        }

        private void RefreshVoucherList()
        {
            using (var context = new MovieManagementContext())
            {
                // Lấy danh sách voucher từ cơ sở dữ liệu
                var vouchers = context.Vouchers.ToList();
                VoucherList = new ObservableCollection<Voucher>(vouchers);
            }
        }

        private void DeleteVoucher(object parameter)
        {
            if (parameter is Voucher voucher)
            {
                using (var context = new MovieManagementContext())
                {
                    // Xóa voucher khỏi cơ sở dữ liệu
                    Debug.WriteLine("Aaaaaaaaaaaaaaaaa");
                    context.Vouchers.Remove(voucher);
                    context.SaveChanges();
                }
                // Cập nhật lại danh sách voucher sau khi xóa
                RefreshVoucherList();
            }
        }

        private void AddVoucher(object parameter)
        {
            AddVoucherView addVoucherView = new AddVoucherView();
            addVoucherView.AllowsTransparency = true;
            addVoucherView.WindowStyle = WindowStyle.None;
            if (addVoucherView.ShowDialog().Value == true)
            {
                using (var context = new MovieManagementContext())
                {
                    // Thêm một voucher mới vào cơ sở dữ liệu
                    var newVoucher = addVoucherView.newVoucher;
                    context.Vouchers.Add(newVoucher);
                    context.SaveChanges();
                }
            }

            // Cập nhật lại danh sách voucher sau khi thêm
            RefreshVoucherList();
        }
    }
}

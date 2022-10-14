using Ninject;
using System;
using System.App.Services;
using System.App.Services.HCNS;
using System.App.Services.Interfaces;
//using System.App.Services.QLCL;
using System.Collections.Generic;
using System.Web.Mvc;

namespace TamAnhHospital.Security
{
    public class Ninject : IDependencyResolver
    {
        private IKernel kernel;

        public Ninject()
        {
            kernel = new StandardKernel();
            BindingResolver();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void BindingResolver()
        {
            kernel.Bind<KhoaPhong_Interface>().To<KhoaPhong_Service>();
           // kernel.Bind<LCD_Interface>().To<LCD_Service>();
            kernel.Bind<TrainingCourse_Interface>().To<TrainingCourse_Service>();
           // kernel.Bind<Vaccine_Interface>().To<Vaccine_Service>();
            kernel.Bind<DoiChieuSuatAn_Interface>().To<DoiChieuSuatAn_Service>();
            kernel.Bind<Employee_Interface>().To<Employee_Service>();
            //kernel.Bind<NLDD_KhoaPhong_Interface>().To<NLDD_KhoaPhong_Service>();
            kernel.Bind<ISKNV>().To<SKNV_Service>();
            kernel.Bind<NhanVien_Interface>().To<NhanVien_Service>();
            kernel.Bind<IKhaiBaoVang>().To<KhaiBaoVang_Service>();
            kernel.Bind<HDLD_Interface>().To<HDLD_Service>();
            kernel.Bind<IBCCC>().To<BCCC_Service>();
            kernel.Bind<IConfig>().To<Config_Service>();
            //HCNS
            kernel.Bind<DK_SuatAn_Interface>().To<DK_SuatAn_Service>();
            kernel.Bind<ChamCong_Interface>().To<ChamCong_Service>();
            kernel.Bind<IHCNS_CongVan>().To<HCNS_CongVan_Service>();
            kernel.Bind<IRoom_Service>().To<Room_Service>();
            //QLCL
            //kernel.Bind<ThoiGianKhamTrungBinh_Interface>().To<ThoiGianKhamTrungBinh_Service>();
            //kernel.Bind<ILichKhamBacSi_Service>().To<LichKhamBacSi_Service>();
            //kernel.Bind<IBieuDo>().To<BieuDo_Service>();
            //DanhMuc
            kernel.Bind<IDanhMuc_Service>().To<DanhMuc_Service>();
            //FO
            //kernel.Bind<IFO_Service>().To<FO_Service>();
        }
    }
}
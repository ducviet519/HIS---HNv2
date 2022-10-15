using System;
using System.App.Entities.Common;
using System.App.Entities.HCNS;
using System.App.Repositories.HCNS;
using System.App.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace System.App.Services.HCNS
{
    
    class DaoTaoChungChi_Services : IDaoTaoChungChi_Services
    {
        private readonly DaoTaoChungChi_Repo _daoTaoChungChi_Repo;
        public DaoTaoChungChi_Services()
        {
            _daoTaoChungChi_Repo = new DaoTaoChungChi_Repo();
        }
        
        public async Task<string> AddDaoTaoAsync(DaoTaoChungChi model)
        {
            return await _daoTaoChungChi_Repo.AddDaoTaoAsync(StaticParams.connectionStringWiseEyeWebOn, model);
        }

        public async Task<string> EditDaoTaoAsync(DaoTaoChungChi model)
        {
            return await _daoTaoChungChi_Repo.EditDaoTaoAsync(StaticParams.connectionStringWiseEyeWebOn, model);
        }

        public async Task<IEnumerable<DaoTaoChungChi>> GetAllDaoTaoListAsync()
        {
            return await _daoTaoChungChi_Repo.GetAllDaoTaoListAsync(StaticParams.connectionStringWiseEyeWebOn);
        }

        public async Task<DaoTaoChungChi> GetDaoTaoByIDAsync(int id)
        {
            return await _daoTaoChungChi_Repo.GetDaoTaoByIDAsync(StaticParams.connectionStringWiseEyeWebOn, id);
        }

        public async Task<IEnumerable<DaoTaoChungChi>> SearchDaoTaoAsync(SearchDaoTao search)
        {
            IEnumerable<DaoTaoChungChi> data = await _daoTaoChungChi_Repo.GetAllDaoTaoListAsync(StaticParams.connectionStringWiseEyeWebOn);
            #region Lọc data theo dữ liệu tìm kiếm
            if (!String.IsNullOrEmpty(search.SearchMaNV))
            {
                data = data.Where(d => d.UserFullCode != null && d.UserFullCode.ToUpper().Contains(search.SearchMaNV.ToUpper())
                                || d.UserFullName != null && StaticParams.ConvertToUnSign(d.UserFullName.ToUpper()).Contains(StaticParams.ConvertToUnSign(search.SearchMaNV.ToUpper()))
                                ).ToList();
            }
            if (!String.IsNullOrEmpty(search.SearchKhoaPhong))
            {
                data = data.Where(d => d.KhoaPhong.ToString().ToUpper().Contains(search.SearchKhoaPhong.ToUpper())).ToList();
            }
            //if (SearchNam != null && SearchNam > 0)
            //{
            //    data = data.Where(d => d.KhoaPhong.ToUpper().Contains(SearchKhoaPhong.ToUpper())).ToList();
            //}
            if (!String.IsNullOrEmpty(search.SearchTrangThai))
            {
                data = data.Where(d => d.TrangThai.ToUpper().Contains(search.SearchTrangThai.ToUpper())).ToList();
            }
            if (!String.IsNullOrEmpty(search.SearchTenCC))
            {
                data = data.Where(d => StaticParams.ConvertToUnSign(d.TrangThai.ToUpper()).Contains(StaticParams.ConvertToUnSign(search.SearchTenCC.ToUpper()))).ToList();
            }
            #endregion

            return data;
        }

        public async Task<HCNS_NhanVien> TimThongTinNhanVienAsync(HCNS_NhanVien nv)
        {
            return await _daoTaoChungChi_Repo.TimThongTinNhanVienAsync(StaticParams.connectionStringWiseEyeWebOn, nv);
        }
    }
}

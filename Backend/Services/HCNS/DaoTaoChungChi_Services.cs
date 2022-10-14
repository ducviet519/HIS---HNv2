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
    
    class DaoTaoChungChi_Services : DaoTaoChungChi_Interface
    {
        private readonly DaoTaoChungChi_Repo _daoTaoChungChi_Repo;
        public DaoTaoChungChi_Services(DaoTaoChungChi_Repo daoTaoChungChi_Repo)
        {
            _daoTaoChungChi_Repo = daoTaoChungChi_Repo;
        }
        
        public async Task<string> AddDaoTaoAsync(DaoTaoChungChi model)
        {
            return await _daoTaoChungChi_Repo.AddDaoTaoAsync(StaticParams.connectionStringWiseEyeWebOn, model);
        }

        public async Task<string> EditDaoTaoAsync(DaoTaoChungChi model)
        {
            return await _daoTaoChungChi_Repo.EditDaoTaoAsync(StaticParams.connectionStringWiseEyeWebOn, model);
        }

        public async Task<List<DaoTaoChungChi>> GetAllDaoTaoListAsync()
        {
            return await _daoTaoChungChi_Repo.GetAllDaoTaoListAsync(StaticParams.connectionStringWiseEyeWebOn);
        }

        public async Task<DaoTaoChungChi> GetDaoTaoByIDAsync(int id)
        {
            return await _daoTaoChungChi_Repo.GetDaoTaoByIDAsync(StaticParams.connectionStringWiseEyeWebOn,id);
        }

        #region Khử dấu cho string        
        public static string convertToUnSign(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
        #endregion
        public async Task<List<DaoTaoChungChi>> SearchDaoTaoAsync(SearchDaoTao search)
        {
            List<DaoTaoChungChi> data = await _daoTaoChungChi_Repo.GetAllDaoTaoListAsync(StaticParams.connectionStringWiseEyeWebOn);
            #region Lọc data theo dữ liệu tìm kiếm
            if (!String.IsNullOrEmpty(search.SearchMaNV))
            {
                data = data.Where(d => d.UserFullCode != null && d.UserFullCode.ToUpper().Contains(search.SearchMaNV.ToUpper())
                                || d.UserFullName != null && convertToUnSign(d.UserFullName.ToUpper()).Contains(convertToUnSign(search.SearchMaNV.ToUpper()))
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
                data = data.Where(d => convertToUnSign(d.TrangThai.ToUpper()).Contains(convertToUnSign(search.SearchTenCC.ToUpper()))).ToList();
            }
            #endregion

            return data;
        }
    }
}

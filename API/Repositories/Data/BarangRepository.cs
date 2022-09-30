using API.Context;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using API.ViewModels;

namespace API.Repositories.Data
{
    public class BarangRepository
    {
        private readonly MyContext _context;

        public BarangRepository(MyContext context)
        {
            _context = context;
        }

        public List<Barang> Get()
        {
            return _context.Barangs.ToList();
        }


        public Barang Get(int id)
        {
            var barang = _context.Barangs.Find(id);

            return barang;
        }


        public int Post(BarangVM barang)
        {

            _context.Barangs.Add(new Barang { Nama=barang.Nama, Satuan=barang.Satuan, Stock=barang.Stock});
            var result =  _context.SaveChanges();

            return result;
        }        
        
  
        public int Pengadaan(PengadaanVM pengadaan)
        {
            using var transaction = _context.Database.BeginTransaction();
            var result = 0;
            int id;
            try
            {
                if (_context.UserRoles.Where(x => x.Id == pengadaan.PetugasId).FirstOrDefault().RoleId == 1)
                {
                    var isExist = _context.Barangs.Where(x => x.Nama == pengadaan.Nama).FirstOrDefault();
                    if (isExist != null)
                    {
                        isExist.Stock = isExist.Stock + pengadaan.Qty;
                        _context.Barangs.Update(isExist);
                        result += _context.SaveChanges();
                        id = isExist.Id;
                    }
                    else
                    {
                        _context.Barangs.Add(new Barang { Nama = pengadaan.Nama, Satuan = pengadaan.Satuan, Stock = pengadaan.Qty });
                        result += _context.SaveChanges();
                        id = _context.Barangs.OrderByDescending(x => x.Id).First().Id;

                    }
                    _context.RiwayatPengadaans.Add(new RiwayatPengadaan { BarangId = id, Supplier = pengadaan.Supplier, Tgl = pengadaan.Tgl, Qty = pengadaan.Qty, Harga = pengadaan.Harga });
                    result += _context.SaveChanges();
                    transaction.Commit();
                }
                else
                {
                    return 0;
                }
                    
                
            }
            catch (Exception)
            {
                transaction.Rollback();
                result = 0;
            }

     
            return result ;

        }


        public int Peminjaman(PeminjamanVM peminjaman)
        {
            using var transaction = _context.Database.BeginTransaction();
            var result = 0;
            try
            {
                //Cek apakah masih ada pinjaman dengan barang dan peminjam yang sama dengan status belum kembali 
                if(_context.RiwayatPeminjamans.Where(x=>x.EmployeeId==peminjaman.EmployeeId && x.Status == "Pinjam" && x.BarangId == peminjaman.BarangId).FirstOrDefault() != null)
                {
                    return 0;
                }

                if(_context.UserRoles.Where(x => x.Id == peminjaman.PetugasId).FirstOrDefault().RoleId == 1)
                {
                    var isExist = _context.Barangs.Find(peminjaman.BarangId);
                    if (isExist != null)
                    {
                        isExist.Stock = isExist.Stock - peminjaman.Qty;
                        _context.Barangs.Update(isExist);
                        result += _context.SaveChanges();

                    }
                    else
                    {
                        return 0;

                    }

                    _context.RiwayatPeminjamans.Add(new RiwayatPeminjaman { BarangId = peminjaman.BarangId, EmployeeId = peminjaman.EmployeeId, Qty = peminjaman.Qty, Status = "Pinjam", Tgl=peminjaman.Tgl});
                    result += _context.SaveChanges();
                    transaction.Commit();
                }
                else
                {
                    return 0;
                }
           
                
            }
            catch (Exception)
            {
                transaction.Rollback();
                result = 0;
            }
            return result;
        }

        public int Pengembalian(PengembalianVM pengembalian)
        {
            using var transaction = _context.Database.BeginTransaction();
            var result = 0;
            try
            {
                if (_context.UserRoles.Where(x => x.Id == pengembalian.PetugasId).FirstOrDefault().RoleId == 1)
                {
                    //Cek apakah masih ada pinjaman dengan barang dan peminjam yang sama dengan status belum kembali 
                    var data = _context.RiwayatPeminjamans.Where(x => x.EmployeeId == pengembalian.EmployeeId && x.Status == "Pinjam" && x.BarangId == pengembalian.BarangId).FirstOrDefault();
                    if ( data != null)
                    {
                        var isExist = _context.Barangs.Find(pengembalian.BarangId);
                        if (isExist != null)
                        {
                            isExist.Stock = isExist.Stock + data.Qty;
                            _context.Barangs.Update(isExist);
                            result += _context.SaveChanges();
                            
                            data.Status = "Kembali";
                            data.Tgl = pengembalian.Tgl;
                            _context.RiwayatPeminjamans.Update(data);
                            result += _context.SaveChanges();
                            transaction.Commit();
                        }
                        else
                        {
                            return 0;

                        }
                        
                    }                    
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                transaction.Rollback();
                result = 0;
            }


            return result;

        }
    }
}

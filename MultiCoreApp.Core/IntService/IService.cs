using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MultiCoreApp.Core.IntService
{
    public interface IService<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id); //asenkron programlama yapmak için task yapısını kullandım,içinde trade sistemleri var
        Task<IEnumerable<T>> GetAllAsync(); // şöylede yapabiliriz :Task<List<T>> GetAllAsync();

        Task<IEnumerable<T>> Where(Expression<Func<T, bool>> predicate); //select * from where Name="Apple" //dönüş tipi olarak boolean bir değer bekliyor fakat tip olarak T tipinde alınacak

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate); //burda tekli bir sonuç döndürür yukarıda çoklu

        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);//buda yularıdakiyle aynı
        Task<IQueryable<T>> QListAsync();

        //update ve delete 'i asenkron olarak yazamam

        Task<T> AddAsync(T entity); //TEK TEK ekleme işlemi
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities); //aynı anda hepsini ekleme işlemi

        T Update(T entity);

        //delete işleminin asenkronsuz hali
        void Remove(T entity); //tek tek silme
        void RemoveRange(IEnumerable<T> entities);//beraber silme

        //delete işleminin asenkronlu hali
        Task DeleteAsync(T entity); //tek tek silme

        Task DeleteRangeAsync(IEnumerable<T> entities); //beraber silme

    }
}

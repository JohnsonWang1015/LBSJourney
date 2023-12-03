namespace LBSJourney.Models
{
    public interface IDaoOperations<T, K>
    {
        T FindById(K id);
        List<T> FindAll();
        Boolean Insert(T source);
        Boolean Update(T source);
        Boolean Delete(K id);
        ConnectionFactory Factory { get; set; }
    }
}

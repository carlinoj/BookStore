﻿namespace BookStore.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    
    Task<T?> GetByIdAsync(Guid id);
    
    Task AddAsync(T entity);
    
    void Update(T entity);
    
    void Remove(T entity);
    
    Task SaveChangesAsync();
}
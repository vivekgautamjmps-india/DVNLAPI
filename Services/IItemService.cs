using DVNLAPI.Models;

namespace DVNLAPI.Services
{
    public interface IItemService
    {
        IEnumerable<Item> GetAll();
        Item? GetById(int id);
        Item Add(ItemCreateDto dto);
        bool Update(int id, ItemUpdateDto dto);
        bool Delete(int id);
    }

    public class InMemoryItemService : IItemService
    {
        private readonly List<Item> _items = new();
        private int _nextId = 1;
        private readonly object _lock = new();

        public InMemoryItemService()
        {
            // seed data
            Add(new ItemCreateDto { Name = "Sample Item 1", Description = "First seed item", Price = 100m });
            Add(new ItemCreateDto { Name = "Sample Item 2", Description = "Second seed item", Price = 250m });
        }

        public IEnumerable<Item> GetAll()
        {
            lock (_lock)
            {
                return _items.OrderBy(i => i.Id).ToList();
            }
        }

        public Item? GetById(int id)
        {
            lock (_lock)
            {
                return _items.FirstOrDefault(i => i.Id == id);
            }
        }

        public Item Add(ItemCreateDto dto)
        {
            lock (_lock)
            {
                var item = new Item
                {
                    Id = _nextId++,
                    Name = dto.Name,
                    Description = dto.Description,
                    Price = dto.Price,
                    CreatedOn = DateTime.UtcNow
                };
                _items.Add(item);
                return item;
            }
        }

        public bool Update(int id, ItemUpdateDto dto)
        {
            lock (_lock)
            {
                var existing = _items.FirstOrDefault(i => i.Id == id);
                if (existing is null) return false;

                existing.Name = dto.Name;
                existing.Description = dto.Description;
                existing.Price = dto.Price;
                return true;
            }
        }

        public bool Delete(int id)
        {
            lock (_lock)
            {
                var existing = _items.FirstOrDefault(i => i.Id == id);
                if (existing is null) return false;
                _items.Remove(existing);
                return true;
            }
        }
    }
}

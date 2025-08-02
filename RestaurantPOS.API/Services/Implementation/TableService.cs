using RestaurantPOS.API.DTOs;
using RestaurantPOS.API.Entities;
using RestaurantPOS.API.Services.GetCurrentUser;
using RestaurantPOS.API.Services.Interfaces;
using RestaurantPOS.API.UnitOfWork.Interfaces;

namespace RestaurantPOS.API.Services.Implementation
{
    public class TableService : ITableService
    {
        private readonly IUnitOfWork _uow;

        public TableService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<TableDto>> GetAllAsync()
        {
            var tables = await _uow.Tables.GetAllAsync();
            return tables.Select(t => new TableDto
            {
                Id = t.Id,
                GlobalId = t.GlobalId,
                TableNumber = t.TableNumber,
                IsOccupied = t.IsOccupied
            });
        }

        public async Task<TableDto> GetByIdAsync(int id)
        {
            var t = await _uow.Tables.GetByIdAsync(id);
            return t == null ? null : new TableDto
            {
                Id = t.Id,
                GlobalId = t.GlobalId,
                TableNumber = t.TableNumber,
                IsOccupied = t.IsOccupied
            };
        }

        public async Task<TableDto> GetByGlobalIdAsync(Guid globalId)
        {
            var t = await _uow.Tables.GetByGlobalIdAsync(globalId);
            return t == null ? null : new TableDto
            {
                Id = t.Id,
                GlobalId = t.GlobalId,
                TableNumber = t.TableNumber,
                IsOccupied = t.IsOccupied
            };
        }

        public async Task<TableDto> CreateOrUpdateAsync(CreateTableDto dto)
        {
            if (!dto.GlobalId.HasValue)
            {
                var table = new Table
                {
                    TableNumber = dto.TableNumber,
                    IsOccupied = dto.IsOccupied
                };
                await _uow.Tables.AddAsync(table);
                await _uow.SaveChangesAsync();

                return new TableDto
                {
                    Id = table.Id,
                    GlobalId = table.GlobalId,
                    TableNumber = table.TableNumber,
                    IsOccupied = table.IsOccupied
                };
            }
            else
            {
                var table = await _uow.Tables.GetByGlobalIdAsync(dto.GlobalId.Value);
                if (table == null) throw new Exception("Table not found");

                table.TableNumber = dto.TableNumber;
                table.IsOccupied = dto.IsOccupied;

                _uow.Tables.Update(table);
                await _uow.SaveChangesAsync();

                return new TableDto
                {
                    Id = table.Id,
                    GlobalId = table.GlobalId,
                    TableNumber = table.TableNumber,
                    IsOccupied = table.IsOccupied
                };
            }
        }

        public async Task DeleteAsync(int id)
        {
            var t = await _uow.Tables.GetByIdAsync(id);
            if (t != null)
            {
                _uow.Tables.Delete(t);
                await _uow.SaveChangesAsync();
            }
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camels.DataAccess;
using Camels.DataAccess.Exceptions;
using Camels.DataAccess.Models;
using Camels.DataAccess.Services;
using Microsoft.EntityFrameworkCore;

namespace Camels.Tests
{
    
    public class CamelTests
    {
        private readonly CamelsDbContext _context;
        private readonly CamelsService _service;

        public CamelTests()
        {
            var options = new DbContextOptionsBuilder<CamelsDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new CamelsDbContext(options);
            _service = new CamelsService(_context);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllCamels()
        {
            var result = await _service.GetAllAsync();

            Assert.Empty(result);

            _context.Camels.AddRange(
                new Camel { Name = "A", HumpCount = 1 },
                new Camel { Name = "B", HumpCount = 2 }
            );

            await _context.SaveChangesAsync();

            result = await _service.GetAllAsync();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetByIdAsync_ExistingCamel_ReturnsCamel()
        {
            var camel = new Camel
            {
                Name = "FindMe",
                HumpCount = 1
            };

            _context.Camels.Add(camel);
            await _context.SaveChangesAsync();

            var result = await _service.GetByIdAsync(camel.Id);

            Assert.Equal("FindMe", result.Name);
        }

        [Fact]
        public async Task GetByIdAsync_NotFound_Throws()
        {
            await Assert.ThrowsAsync<EntityNotFoundException>(() =>
                _service.GetByIdAsync(999));
        }


        [Fact]
        public async Task AddAsync_ValidCamel_AddsSuccessfully()
        {
            var camel = new Camel
            {
                Name = "Test",
                HumpCount = 2
            };

            await _service.AddAsync(camel);

            Assert.NotEqual(0, camel.Id);
        }

        [Fact]
        public async Task AddAsync_EmptyName_Throws()
        {
            var camel = new Camel
            {
                Name = "",
                HumpCount = 1
            };

            await Assert.ThrowsAsync<InvalidDataException>(() =>
                _service.AddAsync(camel));
        }


        [Fact]
        public async Task AddAsync_InvalidHumpCount_Throws()
        {
            var camel = new Camel
            {
                Name = "Bad",
                HumpCount = 3
            };

            await Assert.ThrowsAsync<InvalidDataException>(() =>
                _service.AddAsync(camel));
        }

        [Fact]
        public async Task UpdateAsync_ValidUpdate_UpdatesCamel()
        {
            var camel = new Camel
            {
                Name = "Old",
                HumpCount = 1
            };

            _context.Camels.Add(camel);
            await _context.SaveChangesAsync();

            camel.Name = "New";

            await _service.UpdateAsync(camel);

            var updated = await _context.Camels.FindAsync(camel.Id);

            Assert.Equal("New", updated!.Name);
        }

        [Fact]
        public async Task UpdateAsync_InvalidHumpCount_Throws()
        {
            var camel = new Camel
            {
                Name = "Test",
                HumpCount = 1
            };

            _context.Camels.Add(camel);
            await _context.SaveChangesAsync();

            camel.HumpCount = 5;

            await Assert.ThrowsAsync<InvalidDataException>(() =>
                _service.UpdateAsync(camel));
        }

        [Fact]
        public async Task UpdateAsync_EmptyName_Throws()
        {
            var camel = new Camel
            {
                Name = "Joe",
                HumpCount = 1
            };

            _context.Camels.Add(camel);
            await _context.SaveChangesAsync();

            camel.Name = "";

            await Assert.ThrowsAsync<InvalidDataException>(() =>
                _service.UpdateAsync(camel));
        }

        [Fact]
        public async Task UpdateAsync_NotFound_Throws()
        {
            var camel = new Camel
            {
                Id = 999,
                Name = "Ghost",
                HumpCount = 1
            };

            await Assert.ThrowsAsync<EntityNotFoundException>(() =>
                _service.UpdateAsync(camel));
        }

        [Fact]
        public async Task DeleteAsync_RemovesCamel()
        {
            var camel = new Camel
            {
                Name = "DeleteMe",
                HumpCount = 1
            };

            _context.Camels.Add(camel);
            await _context.SaveChangesAsync();

            await _service.DeleteAsync(camel.Id);

            var exists = await _context.Camels.AnyAsync(c => c.Id == camel.Id);

            Assert.False(exists);
        }

        [Fact]
        public async Task DeleteAsync_NotFound_Throws()
        {
            await Assert.ThrowsAsync<EntityNotFoundException>(() =>
                _service.DeleteAsync(999));
        }
    }
}

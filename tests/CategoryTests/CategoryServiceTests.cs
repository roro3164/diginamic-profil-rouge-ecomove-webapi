using Ecomove.Api.Data.Models;
using Ecomove.Api.DTOs.CategoryDTOs;
using Ecomove.Api.Helpers;
using Ecomove.Api.Interfaces.IRepositories;
using Ecomove.Api.Services.Categories;
using ErrorOr;
using FluentAssertions;
using Mapster;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace tests.CategoryTests
{
    public class CategoryServiceTests
    {
        private readonly CategoryService _sut; // System Under Test
        private readonly ICategoryRepository categoryRepository = Substitute.For<ICategoryRepository>();
        private readonly ILogger<CategoryService> logger = Substitute.For<ILogger<CategoryService>>();

        public CategoryServiceTests()
        {
            _sut = new CategoryService(categoryRepository, logger);
        }

        [Fact]
        public async Task CreateCategoryAsync_ShouldReturnCreated_WhenUserCreatedSuccessfully()
        {
            // Arrange
            CategoryDTO categoryData = new CategoryDTO { CategoryLabel = "category test" };


            // Configure CreateCategoryAsync to return Result.Created as result
            categoryRepository.CreateCategoryAsync(categoryData).Returns(Result.Created);

            // Act
            Response<bool> result = await _sut.CreateCategoryAsync(categoryData);

            // Assert
            result.Should().BeEquivalentTo(new Response<bool>
            {
                IsSuccess = true,
                Message = "La catégorie a bien été créée avec succès",
                CodeStatus = 201,
                Data = true
            });
        }

        [Fact]
        public async Task GetAllCategoriesAsync_ShouldReturnEmptyList_WhenNoCategoryExist()
        {
            // Configure GetAllCategoriesAsync to return an empty list
            categoryRepository.GetAllCategoriesAsync().Returns(Enumerable.Empty<Category>().ToList());

            // Act
            Response<List<CategoryDTO>> result = await _sut.GetAllCategoriesAsync();

            // Assert

            result.Should().BeEquivalentTo(
                new Response<List<CategoryDTO>>
                {
                    IsSuccess = true,
                    CodeStatus = 200,
                    Data = Enumerable.Empty<CategoryDTO>().ToList()
                }
            );
        }

        [Fact]
        public async Task GetCategoryByIdAsync_ShouldReturnCategoryByID_WhenTheCategoryExists()
        {
            Category expectedCategory = new Category() { CategoryId = 1, CategoryLabel = "Category test" };

            // Configure GetCategoryByIdAsync to return the expected category
            categoryRepository.GetCategoryByIdAsync(1).Returns(expectedCategory);

            // Act
            Response<CategoryDTO> result = await _sut.GetCategoryByIdAsync(1);

            // Assert
            result.Should().BeEquivalentTo(new Response<CategoryDTO>
            {
                IsSuccess = true,
                CodeStatus = 200,
                Data = expectedCategory.Adapt<CategoryDTO>()
            });
        }
    }
}
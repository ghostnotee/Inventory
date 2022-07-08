using AutoMapper;
using Inventory.Application.Features.Queries.Categories;
using Inventory.Application.Interfaces.Repositories;
using Inventory.Domain.Entities;
using MediatR;

namespace Inventory.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommand : IRequest<CategoryViewModel>
{
    public string Name { get; set; }
    public string ParentCategoryId { get; set; }
}

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryViewModel>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<CategoryViewModel> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category { Name = request.Name, ParentCategoryId = request.ParentCategoryId};
        await _categoryRepository.AddAsync(category);
        return _mapper.Map<CategoryViewModel>(category);
    }
}
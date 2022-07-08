using AutoMapper;
using Inventory.Application.Features.Queries.Categories;
using Inventory.Application.Interfaces.Repositories;
using MediatR;

namespace Inventory.Application.Features.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommand : IRequest<CategoryViewModel>
{
    public string Id { get; set; }
}

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, CategoryViewModel>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public DeleteCategoryCommandHandler(IMapper mapper, ICategoryRepository categoryRepository)
    {
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryViewModel> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var deletedCategory = await _categoryRepository.DeleteAsync(request.Id);
        return _mapper.Map<CategoryViewModel>(deletedCategory);
    }
}
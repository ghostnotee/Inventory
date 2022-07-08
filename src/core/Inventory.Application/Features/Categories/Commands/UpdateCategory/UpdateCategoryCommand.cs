using AutoMapper;
using Inventory.Application.Exceptions;
using Inventory.Application.Features.Queries.Categories;
using Inventory.Application.Interfaces.Repositories;
using Inventory.Domain.Entities;
using MediatR;

namespace Inventory.Application.Features.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommand : IRequest<CategoryViewModel>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string ParentCategoryId { get; set; }
}

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryViewModel>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<CategoryViewModel> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var dbCategory = await _categoryRepository.GetByIdAsync(request.Id);
        if (dbCategory is null) throw new NotFoundException(nameof(Category), request.Id);

        dbCategory.Name = request.Name;
        dbCategory.ParentCategoryId = request.ParentCategoryId;
        dbCategory.UpdateDate = DateTime.Now;
        await _categoryRepository.UpdateAsync(request.Id, dbCategory);
        return _mapper.Map<CategoryViewModel>(dbCategory);
    }
}
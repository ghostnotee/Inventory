using AutoMapper;
using Inventory.Application.Interfaces.Repositories;
using MediatR;

namespace Inventory.Application.Features.Categories.Queries.GetAllCategories;

public class GetAllCategoriesQuery : IRequest<List<CategoryViewModel>>
{
}

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, List<CategoryViewModel>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<List<CategoryViewModel>> Handle(GetAllCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var dbCategories = _categoryRepository.Get().ToList();
        return await Task.FromResult(_mapper.Map<List<CategoryViewModel>>(dbCategories));
    }
}
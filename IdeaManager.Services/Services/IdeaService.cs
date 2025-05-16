using IdeaManager.Core.Entities;
using IdeaManager.Core.Enums;
using IdeaManager.Core.Interfaces;

public class IdeaService : IIdeaService
{
    private readonly IUnitOfWork _unitOfWork;

    public IdeaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task SubmitIdeaAsync(Idea idea)
    {
        if (string.IsNullOrWhiteSpace(idea.Title))
            throw new ArgumentException("Le titre est obligatoire.");

        idea.VoteCount = 0;
        idea.Status = IdeaStatus.Pending;

        await _unitOfWork.IdeaRepository.AddAsync(idea);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<List<Idea>> GetAllAsync()
    {
        return await _unitOfWork.IdeaRepository.GetAllAsync();
    }

    public async Task VoteForIdeaAsync(int ideaId)
    {
        var idea = await _unitOfWork.IdeaRepository.GetByIdAsync(ideaId);
        if (idea == null)
            throw new InvalidOperationException("Idée non trouvée.");

        idea.VoteCount++;
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task ApproveIdeaAsync(Idea idea)
    {
        var ideaFromDb = await _unitOfWork.IdeaRepository.GetByIdAsync(idea.Id);
        if (ideaFromDb == null)
            throw new InvalidOperationException("Idée non trouvée.");

        ideaFromDb.Status = IdeaStatus.Approved;
        ideaFromDb.VoteCount = 1;

        await _unitOfWork.SaveChangesAsync();
    }


    public async Task RejectIdeaAsync(Idea idea)
    {
        var ideaFromDb = await _unitOfWork.IdeaRepository.GetByIdAsync(idea.Id);
        if (ideaFromDb == null)
            throw new InvalidOperationException("Idée non trouvée.");

        ideaFromDb.Status = IdeaStatus.Rejected;
        ideaFromDb.VoteCount = 0;

        await _unitOfWork.SaveChangesAsync();
    }


}

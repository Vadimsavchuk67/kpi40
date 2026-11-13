using PostService.CommonTypes;
using PostService.Models;

namespace PostService.BusinessLogic;

public class PostingService : IPostingService
{
    private static List<Posting> postings = new List<Posting>();

    public Posting Create(Posting newPosting)
    {
        newPosting.Id = postings.Count == 0 ? 1 : postings.Max(p => p.Id) + 1;
        newPosting.CreatedAt = DateTime.UtcNow;
        float rate = 10;
        if (newPosting.DeliveryType == DeliveryType.Courier)
            rate = 20;
        else if (newPosting.DeliveryType == DeliveryType.ExpressCourier)
            rate = 30;

        newPosting.Price = newPosting.Weight * rate;
        postings.Add(newPosting);
        return newPosting;
    }

    public List<Posting> GetAll()
    {
        return postings;
    }

    public Posting? Find(int postingId)
    {
        return postings.FirstOrDefault(p => p.Id == postingId);
    }

    public Posting? Update(Posting updated)
    {
        var posting = Find(updated.Id);
        if (posting == null)
            return null;

        posting.From = updated.From;
        posting.To = updated.To;
        posting.Content = updated.Content;
        posting.DeliveryType = updated.DeliveryType;
        posting.Weight = updated.Weight;
        posting.Width = updated.Width;
        posting.Height = updated.Height;
        posting.Depth = updated.Depth;
        posting.Value = updated.Value;
        posting.Price = updated.Price;

        return posting;
    }

    public int Delete(int postingId)
    {
        var posting = Find(postingId);
        if (posting == null)
            return 0;

        postings.Remove(posting);
        return 1;
    }
}

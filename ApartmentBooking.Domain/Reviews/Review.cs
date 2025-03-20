using ApartmentBooking.Domain.Abstraction;
using ApartmentBooking.Domain.Reviews.DomainEvents;

namespace ApartmentBooking.Domain.Reviews;

public sealed class Review : Entity
{
    private Review(Guid id, Guid userId, Guid apartmentId, Rating rating, Comment comment,DateTime createdAt) : base(id)
    {
        UserId = userId;
        ApartmentId = apartmentId;
        Rating = rating;
        Comment = comment;
        CreatedAt = createdAt;
    }
    private Review()
    {
        
    }
    public Guid UserId { get; private set; }
    public Guid ApartmentId { get; private set; }
    public Rating Rating { get; private set; }
    public Comment Comment {  get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Review CreateReview (Guid id, Guid userId, Guid apartmentId, Rating rating, Comment comment)
    {
        var review = new Review(id, userId, apartmentId, rating, comment, DateTime.UtcNow);

        RaiseDomainEvent(new ReviewCreatedDomainEvent(review.Id));
        return review;
    }

}

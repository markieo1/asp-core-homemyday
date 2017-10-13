using HomeMyDay.Core.Models;
using HomeMyDay.Core.Repository;
using HomeMyDay.Core.Services;
using HomeMyDay.Web.Base.ViewModels;
using Microsoft.Extensions.Options;

namespace HomeMyDay.Web.Base.BaseControllers
{
	public class AccommodationBaseController
    {
	    private readonly IAccommodationRepository _accommodationRepository;
	    private readonly IReviewRepository _reviewRepository;
	    private readonly GoogleApiServiceOptions _googleOptions;

	    protected AccommodationBaseController(IAccommodationRepository accommodationRepository, IReviewRepository reviewRepository, IOptions<GoogleApiServiceOptions> googleOpts)
	    {
		    _accommodationRepository = accommodationRepository;
		    _reviewRepository = reviewRepository;
		    _googleOptions = googleOpts.Value;
	    }

	    protected AccommodationViewModel GetAccommodation(long id)
	    {
		    return new AccommodationViewModel()
		    {
			    Accommodation = _accommodationRepository.GetAccommodation(id),
				Reviews = _reviewRepository.GetAccomodationReviews(id)
		    };
	    }
    }
}

using AutoMapper;
using Chariot.Engine.DataObject.MardisCommon;
using Chariot.Engine.DataObject.MardisCore;
using Chariot.Engine.DataObject.MardisOrders;
using Chariot.Framework.MardiscoreViewModel;
using Chariot.Framework.MardisOrdersViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.Helpers
{
    public  class Mappers : Profile
    {
        public Mappers()
        {

            //// Add as many of these lines as you need to map your objects
            //CreateMap<Service, MyTaskServicesViewModel>()
            //                  .ForMember(dest => dest.ServiceDetailCollection, opt => opt.MapFrom(src => src.ServiceDetails.OrderBy(sd => sd.Order)));
            //CreateMap<ServiceDetail, MyTaskServicesDetailViewModel>()
            //    .ForMember(dest => dest.QuestionCollection, opt => opt.MapFrom(src => src.Questions.OrderBy(q => q.Order)))
            //    .ForMember(dest => dest.Sections, opt => opt.MapFrom(src => src.Sections.OrderBy(s => s.Order)));
            //CreateMap<Question, MyTaskQuestionsViewModel>()
            //    //   .ForMember(dest => dest.HasPhoto, opt => opt.MapFrom(src => src.HasPhoto.IndexOf("S", StringComparison.Ordinal) >= 0))
            //    .ForMember(dest => dest.QuestionDetailCollection, opt => opt.MapFrom(src => src.QuestionDetails.OrderBy(qd => qd.Order)))
            //    .ForMember(dest => dest.CodeTypePoll, opt => opt.MapFrom(src => src.TypePoll.Code));
            //CreateMap<QuestionDetail, MyTaskQuestionDetailsViewModel>();
            //CreateMap<TaskPerCampaignViewModel, TaskCampaigViewModel>();
            //CreateMap<Branch, BranchItemViewModel>();
            //CreateMap<AnswerDetailSecondLevel, MytaskAnwerDetailSecondModel>();
            //CreateMap<MytaskAnwerDetailSecondModel, AnswerDetailSecondLevel>();
            //CreateMap<PollsterRegisterViewModel, Pollster>();
            //CreateMap<viewEjercicio, Ejercicio>();
            CreateMap<TrackingViewModel, PersonalTraker>();
            CreateMap<TrackingBranchViewModel, TrackingBranch>();
            CreateMap<Salesman, VendedoresViewModel>();

            CreateMap<Items, RubrosViewModel>();
            CreateMap<Client, ClientViewModel>();
            CreateMap<Product, ArticulosViewModel>();
            CreateMap<Deposit, DepositosViewModel>();
            CreateMap<ClientViewModel, Client>();
            CreateMap<VisitasViewModel, Visitas>();
            CreateMap<OrdersViewModel, Order>()
                  
          .ForMember(dest => dest.pedidosItems, opt => opt.MapFrom(src => src.pedidosItems));

            CreateMap<InventaryViewModel, Inventory>()
          .ForMember(dest => dest.inventariodetalles, opt => opt.MapFrom(src => src.inventariodetalles));
            //.ForMember(x=>x.Id, opt =>opt.Ignore());


        }
    }
}

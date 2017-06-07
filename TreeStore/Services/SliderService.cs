using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeStore.Data.Interface;
using TreeStore.Data.Repositories;
using TreeStore.Models.Entities;

namespace TreeStore.Services
{
    public interface ISliderService
    {

        IEnumerable<Slider> GetSliders();

        List<Slider> GetSliders(string User, long id);
        Slider GetSlider(long id);
        void CreateSlider(Slider Slider);
        void UpdateSlider(Slider Slider);
        void DeleteSlider(long id);
        int CountSlider();
        void SaveSlider();
    }

    public class SliderService : ISliderService
    {
        private readonly ISliderRepository SliderRepository;
        private readonly IUnitOfWork unitOfWork;

        #region ISliderService Members
        public SliderService(ISliderRepository SliderRepository, IUnitOfWork unitOfWork)
        {
            this.SliderRepository = SliderRepository;
            this.unitOfWork = unitOfWork;
        }
        public int CountSlider()
        {
            return SliderRepository.GetAll().Count();
        }

        public void CreateSlider(Slider Slider)
        {
            SliderRepository.Add(Slider);
        }

        public void DeleteSlider(long id)
        {
            SliderRepository.Delete(c => c.Id == id);
        }

        public List<Slider> GetSliders(string User, long id)
        {
            return SliderRepository.GetMany(c => c.CreatedBy == User && c.Id == id).ToList();
        }

        public IEnumerable<Slider> GetSliders()
        {
            var Sliders = SliderRepository.GetAll();
            return Sliders;

        }

        public Slider GetSlider(long id)
        {
            var Slider = SliderRepository.GetById(id);
            return Slider;
        }

        public void SaveSlider()
        {
            unitOfWork.Commit();
        }


        public void UpdateSlider(Slider Slider)
        {
            SliderRepository.Update(Slider);
        }
        #endregion
    }
}

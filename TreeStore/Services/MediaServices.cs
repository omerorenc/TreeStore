using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeStore.Data.Interface;
using TreeStore.Data.Repositories;
using TreeStore.Models.Entities;

namespace TreeStore.Services
{
    public interface IMediaService
    {

        IEnumerable<Media> GetMedias();

        List<Media> GetMedias(string User, long id);
        Media GetMedia(long id);
        void CreateMedia(Media Media);
        void UpdateMedia(Media Media);
        void DeleteMedia(long id);
        int CountMedia();
        void SaveMedia();
    }

    public class MediaService : IMediaService
    {
        private readonly IMediaRepository MediaRepository;
        private readonly IUnitOfWork unitOfWork;

        #region IMediaService Members
        public MediaService(IMediaRepository _MediaRepository, IUnitOfWork unitOfWork)
        {
            this.MediaRepository = _MediaRepository;
            this.unitOfWork = unitOfWork;
        }
        public int CountMedia()
        {
            return MediaRepository.GetAll().Count();
        }

        public void CreateMedia(Media Media)
        {
            MediaRepository.Add(Media);
        }

        public void DeleteMedia(long id)
        {
            MediaRepository.Delete(c => c.Id == id);
        }

        public Media GetMedia(long id)
        {
            var media = MediaRepository.GetById(id);
            return media;
        }

        public IEnumerable<Media> GetMedias()
        {
            var medias = MediaRepository.GetAll();
            return medias;
        }

        public List<Media> GetMedias(string User, long id)
        {
            return MediaRepository.GetMany(c => c.CreatedBy == User && c.Id == id).ToList();
        }

        public void SaveMedia()
        {
            unitOfWork.Commit();
        }

        public void UpdateMedia(Media Media)
        {
            MediaRepository.Update(Media);
        }

        #endregion
    }
}

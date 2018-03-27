using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects;

namespace WOPR.UI.WPF.Simulation.Models
{
    public abstract class ModelBase<TEntity> : IModel<TEntity>
        where TEntity : BaseDO
    {
        private TEntity m_Entity;

        public event PropertyChangedEventHandler PropertyChanged;

        public TEntity Entity
        {
            get
            {
                return m_Entity;
            }
            private set
            {
                m_Entity = value;
                OnPropertyChanged("Entity");
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ModelBase(TEntity entity)
        {
            if (entity == null)
            {
                throw new NullReferenceException("The entity cannot be null");
            }
            this.Entity = entity;
        }
    }

    public interface IModel<TEntity> : INotifyPropertyChanged
        where TEntity : BaseDO
    {
        TEntity Entity { get; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace Chariot.Engine.DataObject.MardisOrders
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
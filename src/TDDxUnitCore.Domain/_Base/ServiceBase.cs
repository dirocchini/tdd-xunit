using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace TDDxUnitCore.Domain._Base
{
    public class ServiceBase
    {
        protected readonly IMapper _mapper;

        public ServiceBase(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}

using System.Linq.Expressions;
using Magnus.Core.Entities;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Magnus.Infrastructure.Persistence.Repositories;

public class LaboratoryRepository(MagnusContext context) : Repository<Laboratory>(context), ILaboratoryRepository{}
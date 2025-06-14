﻿using NorthWind.Validation.Entities.Enums;
using NorthWind.Validation.Entities.Interfaces;
using NorthWind.Validation.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.Validation.Entities.Abstractions
{
	public abstract class AbstractModelValidator<T>(
	 IValidationService<T> validationService,
	 ValidationConstraint constraint =
		ValidationConstraint.AlwaysValidate
	 ) : IModelValidator<T>
	{
		public ValidationConstraint Constraint => constraint;
		public IEnumerable<ValidationError> Errors { get; private set; }
		public async Task<bool> Validate(T model)
		{
			Errors = await validationService.Validate(model);
			return Errors == default;
		}
		protected IValidationRules<T, TProperty> AddRuleFor<TProperty>(
	   Expression<Func<T, TProperty>> expression) =>
	   validationService.AddRuleFor<TProperty>(expression);
		protected ICollectionValidationRules<T, TProperty> AddRuleForEach<TProperty>(
	   Expression<Func<T, IEnumerable<TProperty>>> expression) =>
	   validationService.AddRuleForEach<TProperty>(expression);
		public IValidationService<T> ValidatorService => validationService;
	}
}

using CompanyManager.Domain.Common;

namespace CompanyManager.Domain.Errors;

public static class DomainErrors
{
	public static class LeaveApplication
	{
		public static readonly Error StartDateMustBeBeforeEndDate = new(
			"LeaveApplication.StartDateMustBeBeforeEndDate",
			"Start date must be before end date.");
		
		public static readonly Error WorkDaysCountMustBeGreaterThanZero = new(
			"LeaveApplication.WorkDaysCountMustBeGreaterThanZero",
			"Work days count must be greater than zero.");

		public static readonly Error OnlyPendingLeaveApplicationsCanBeApproved = new(
			"LeaveApplication.OnlyPendingLeaveApplicationsCanBeApproved",
			"Only pending leave applications can be approved.");
		
		public static readonly Error OnlyPendingLeaveApplicationsCanBeRejected = new(
			"LeaveApplication.OnlyPendingLeaveApplicationsCanBeRejected",
			"Only pending leave applications can be rejected.");
		
		public static readonly Error LeaveApplicationNotFound = new(
			"LeaveApplication.LeaveApplicationNotFound",
			"Leave application not found.");
	}
	
	public static class LeaveApplicationComment
	{
		public static readonly Error CommentBodyCannotBeEmpty = new(
			"LeaveApplicationComment.CommentBodyCannotBeEmpty",
			"Comment body cannot be empty.");
	}
	
	public static class Company
	{
		public static readonly Error CompanyNameCannotBeEmpty = new(
			"Company.CompanyNameCannotBeEmpty",
			"Company name cannot be empty.");
		
		public static readonly Error CompanyNotFound = new(
			"Company.CompanyNotFound",
			"Company not found.");
	}
	
	public static class Employee
	{
		public static readonly Error EmployeeNotFound = new(
			"Employee.EmployeeNotFound",
			"Employee not found.");
	}
	
	public static class EmployeeEmploymentContract
	{
		public static readonly Error CompanyNameCannotBeEmpty = new(
			"EmployeeEmploymentContract.CompanyNameCannotBeEmpty",
			"Company name cannot be empty.");
		
		public static readonly Error StartDateCannotBeLaterThanEndDate = new(
			"EmployeeEmploymentContract.StartDateCannotBeLaterThanEndDate",
			"Start date cannot be later than end date.");
	}
	
	public static class EmployeeSchool
	{
		public static readonly Error StartDateCannotBeLaterThanEndDate = new(
			"EmployeeSchool.StartDateCannotBeLaterThanEndDate",
			"Start date cannot be later than end date.");
	}
}
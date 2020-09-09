Imports DevExpress.ExpressApp.Model
Imports System
Imports System.ComponentModel

Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.DC
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl

Namespace ActionAttributeExample.Module.BusinessObjects
	<DefaultClassOptions, ImageName("BO_Task"), DefaultProperty("Subject")>
	Public Class Task
		Inherits BaseObject

		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub
'INSTANT VB NOTE: The field subject was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private subject_Conflict As String
		Public Property Subject() As String
			Get
				Return subject_Conflict
			End Get
			Set(ByVal value As String)
				SetPropertyValue("Subject", subject_Conflict, value)
			End Set
		End Property
'INSTANT VB NOTE: The field deadline was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private deadline_Conflict? As DateTime
		Public Property Deadline() As DateTime?
			Get
				Return deadline_Conflict
			End Get
			Set(ByVal value? As DateTime)
				SetPropertyValue("Deadline", deadline_Conflict, value)
			End Set
		End Property
'INSTANT VB NOTE: The field isCompleted was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private isCompleted_Conflict As Boolean
		Public Property IsCompleted() As Boolean
			Get
				Return isCompleted_Conflict
			End Get
			Set(ByVal value As Boolean)
				SetPropertyValue("IsCompleted", isCompleted_Conflict, value)
			End Set
		End Property
'INSTANT VB NOTE: The field comments was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private comments_Conflict As String
		<Size(SizeAttribute.Unlimited), ModelDefault("AllowEdit", "False")>
		Public Property Comments() As String
			Get
				Return comments_Conflict
			End Get
			Set(ByVal value As String)
				SetPropertyValue("Comments", comments_Conflict, value)
			End Set
		End Property
		<Action(Caption:="Complete", TargetObjectsCriteria := "Not [IsCompleted]")>
		Public Sub Complete()
			IsCompleted = True
		End Sub
		<Action(Caption := "Postpone", TargetObjectsCriteria := "[Deadline] Is Not Null And Not [IsCompleted]")>
		Public Sub Postpone(ByVal parameters As PostponeParametersObject)
			If Deadline.HasValue AndAlso Not IsCompleted AndAlso (parameters.PostponeForDays > 0) Then
				Deadline += TimeSpan.FromDays(parameters.PostponeForDays)
				Comments &= String.Format("Postponed for {0} days, new deadline is {1:d}" & vbCrLf & "{2}" & vbCrLf, parameters.PostponeForDays, Deadline, parameters.Comment)
			End If
		End Sub
	End Class
	<DomainComponent>
	<ModelDefault("Caption", "Postpone Parameters")>
	Public Class PostponeParametersObject
		Public Sub New()
			PostponeForDays = 1
		End Sub
		Public Property PostponeForDays() As UInteger
		<Size(SizeAttribute.Unlimited)>
		Public Property Comment() As String
	End Class
End Namespace

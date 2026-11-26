# 3. Testing Code

Test folder: https://github.com/AIU-SoftWave/Aspirra/tree/develop/tests

Framework: xUnit with Moq (the .NET equivalents of JUnit and Mockito). Every test method name carries its test case ID: `TC-02.1-01` becomes `TC_02_1_01_<Scenario>`.

| Service | Test project |
|---|---|
| AcademicService | [AcademicService.Tests](https://github.com/AIU-SoftWave/Aspirra/tree/develop/tests/AcademicService.Tests) |
| AssessmentService | [AssessmentService.Tests](https://github.com/AIU-SoftWave/Aspirra/tree/develop/tests/AssessmentService.Tests) |
| CourseService | [CourseService.Tests](https://github.com/AIU-SoftWave/Aspirra/tree/develop/tests/CourseService.Tests) |
| GoalPlannerService | [GoalPlannerService.Tests](https://github.com/AIU-SoftWave/Aspirra/tree/develop/tests/GoalPlannerService.Tests) |
| HabitService | [HabitService.Tests](https://github.com/AIU-SoftWave/Aspirra/tree/develop/tests/HabitService.Tests) |
| StudentService | [StudentService.Tests](https://github.com/AIU-SoftWave/Aspirra/tree/develop/tests/StudentService.Tests) |

## Test case → method map

| TC ID | FR | Method | Source |
|-----|-----|----------------------------------------|---------------|
| TC-02.1-01 | FR-02.1 | `TC_02_1_01_Health_ReturnsHealthy` | [AcademicApiTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/AcademicApiTests.cs) |
| TC-02.1-02 | FR-02.1 | `TC_02_1_02_CreateGoal_ReturnsCreatedGoalAndCanBeRetrieved` | [AcademicApiTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/AcademicApiTests.cs) |
| TC-02.1-03 | FR-02.1 | `TC_02_1_03_CreateGoal_MissingTitle_ReturnsRequiredFieldPayload` | [AcademicApiTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/AcademicApiTests.cs) |
| TC-02.1-04 | FR-02.1 | `TC_02_1_04_CreateGoal_WithoutStudentId_DoesNotPersist` | [GoalMoqTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalMoqTests.cs) |
| TC-02.1-05 | FR-02.1 | `TC_02_1_05_CreateGoal_WithValidData_SetsStatusActive` | [GoalServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalServiceTests.cs) |
| TC-02.1-06 | FR-02.1 | `TC_02_1_06_CreateGoal_MissingTitle_ReturnsRequiredFieldError` | [GoalServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalServiceTests.cs) |
| TC-02.1-07 | FR-02.1 | `TC_02_1_07_CreateGoal_MissingPriority_ReturnsRequiredFieldError` | [GoalServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalServiceTests.cs) |
| TC-02.1-08 | FR-02.1 | `TC_02_1_08_CreateGoal_PastTargetDate_ReturnsInvalidDateError` | [GoalServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalServiceTests.cs) |
| TC-02.1-09 | FR-02.1 | `TC_02_1_09_GetGoals_FiltersByStudentAndStatus` | [GoalServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalServiceTests.cs) |
| TC-02.1-10 | FR-02.1 | `TC_02_1_10_GetGoal_NotFound_ReturnsNotFound` | [GoalServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalServiceTests.cs) |
| TC-02.2-01 | FR-02.2 | `TC_02_2_01_UpdateGoal_ChangesFields` | [GoalServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalServiceTests.cs) |
| TC-02.2-02 | FR-02.2 | `TC_02_2_02_UpdateGoal_MissingTitle_KeepsOldValues` | [GoalServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalServiceTests.cs) |
| TC-02.3-01 | FR-02.3 | `TC_02_3_01_UpdateGoal_PastTargetDate_KeepsOldValues` | [GoalServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalServiceTests.cs) |
| TC-02.4-01 | FR-02.4 | `TC_02_4_01_CompleteGoal_WhenArchived_ReturnsInvalidTransition` | [AcademicApiTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/AcademicApiTests.cs) |
| TC-02.4-02 | FR-02.4 | `TC_02_4_02_CompleteGoal_WhenMissing_ReturnsNotFound` | [AcademicApiTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/AcademicApiTests.cs) |
| TC-02.4-03 | FR-02.4 | `TC_02_4_03_CompleteGoal_FromActive_PersistsExactlyOnce` | [GoalMoqTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalMoqTests.cs) |
| TC-02.4-04 | FR-02.4 | `TC_02_4_04_CompleteGoal_FromArchived_DoesNotPersist` | [GoalMoqTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalMoqTests.cs) |
| TC-02.4-05 | FR-02.4 | `TC_02_4_05_CompleteGoal_SetsCompletedAndCompletionDate` | [GoalServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalServiceTests.cs) |
| TC-02.4-06 | FR-02.4 | `TC_02_4_06_CompleteGoal_Twice_KeepsFirstCompletionDate` | [GoalServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalServiceTests.cs) |
| TC-02.4-07 | FR-02.4 | `TC_02_4_07_CompleteGoal_FromActive_ReturnsCompleted` | [GoalStateTransitionTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalStateTransitionTests.cs) |
| TC-02.4-08 | FR-02.4 | `TC_02_4_08_CompleteGoal_FromCompleted_IsIdempotent` | [GoalStateTransitionTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalStateTransitionTests.cs) |
| TC-02.4-09 | FR-02.4 | `TC_02_4_09_CompleteGoal_FromArchived_ThrowsInvalidTransitionAndDoesNotUpdate` | [GoalStateTransitionTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalStateTransitionTests.cs) |
| TC-02.4-10 | FR-02.4 | `TC_02_4_10_CompleteGoal_MissingGoal_ThrowsNotFound` | [GoalStateTransitionTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalStateTransitionTests.cs) |
| TC-02.4-11 | FR-02.4 | `TC_02_4_11_CompleteGoal_InvalidTransition_ReturnsBadRequestWithCode` | [GoalStateTransitionTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalStateTransitionTests.cs) |
| TC-02.4-12 | FR-02.4 | `TC_02_4_12_CompleteGoal_MissingGoal_ReturnsNotFound` | [GoalStateTransitionTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalStateTransitionTests.cs) |
| TC-02.5-01 | FR-02.5 | `TC_02_5_01_RestoreGoal_FromArchived_ClearsCompletionDateAndPersists` | [GoalMoqTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalMoqTests.cs) |
| TC-02.5-02 | FR-02.5 | `TC_02_5_02_ArchiveAndRestoreGoal_ChangesStatus` | [GoalServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalServiceTests.cs) |
| TC-02.5-03 | FR-02.5 | `TC_02_5_03_ArchiveGoal_FromActive_ReturnsArchived` | [GoalStateTransitionTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalStateTransitionTests.cs) |
| TC-02.5-04 | FR-02.5 | `TC_02_5_04_ArchiveGoal_FromCompleted_ReturnsArchived` | [GoalStateTransitionTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalStateTransitionTests.cs) |
| TC-02.5-05 | FR-02.5 | `TC_02_5_05_RestoreGoal_FromArchived_ReturnsActive` | [GoalStateTransitionTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalStateTransitionTests.cs) |
| TC-02.5-06 | FR-02.5 | `TC_02_5_06_RestoreGoal_AfterArchivedCompletedGoal_ClearsCompletionDate` | [GoalStateTransitionTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalStateTransitionTests.cs) |
| TC-02.5-07 | FR-02.5 | `TC_02_5_07_RestoreGoal_FromActive_ThrowsInvalidTransitionAndDoesNotUpdate` | [GoalStateTransitionTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalStateTransitionTests.cs) |
| TC-02.5-08 | FR-02.5 | `TC_02_5_08_RestoreGoal_FromCompleted_ThrowsInvalidTransitionAndDoesNotUpdate` | [GoalStateTransitionTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalStateTransitionTests.cs) |
| TC-02.5-09 | FR-02.5 | `TC_02_5_09_ArchiveGoal_MissingGoal_ThrowsNotFound` | [GoalStateTransitionTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalStateTransitionTests.cs) |
| TC-02.5-10 | FR-02.5 | `TC_02_5_10_RestoreGoal_MissingGoal_ThrowsNotFound` | [GoalStateTransitionTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalStateTransitionTests.cs) |
| TC-02.5-11 | FR-02.5 | `TC_02_5_11_RestoreGoal_InvalidTransition_ReturnsBadRequestWithCode` | [GoalStateTransitionTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalStateTransitionTests.cs) |
| TC-02.6-01 | FR-02.6 | `TC_02_6_01_DeleteGoal_RemovesGoal` | [GoalServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalServiceTests.cs) |
| TC-04.1-01 | FR-04.1 | `TC_04_1_01_AddAssignment_WithRequiredFields_SetsNotStarted` | [AssessmentServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AssessmentService.Tests/AssessmentServiceTests.cs) |
| TC-04.1-02 | FR-04.1 | `TC_04_1_02_AddAssignment_MissingTitle_ReturnsRequiredFieldError` | [AssessmentServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AssessmentService.Tests/AssessmentServiceTests.cs) |
| TC-04.1-03 | FR-04.1 | `TC_04_1_03_GetAssignmentsByCourse_ReturnsExpectedShape` | [AssessmentServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AssessmentService.Tests/AssessmentServiceTests.cs) |
| TC-04.2-01 | FR-04.2 | `TC_04_2_01_AddExam_WithValidData_IsAddedToUpcoming` | [AssessmentServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AssessmentService.Tests/AssessmentServiceTests.cs) |
| TC-04.2-02 | FR-04.2 | `TC_04_2_02_AddExam_PastDate_ReturnsInvalidDateError` | [AssessmentServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AssessmentService.Tests/AssessmentServiceTests.cs) |
| TC-04.2-03 | FR-04.2 | `TC_04_2_03_GetAssessments_FiltersByStudent` | [AssessmentServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AssessmentService.Tests/AssessmentServiceTests.cs) |
| TC-04.3-01 | FR-04.3 | `TC_04_3_01_SetAssignmentStatus_ToCompleted_UpdatesStatus` | [AssessmentServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AssessmentService.Tests/AssessmentServiceTests.cs) |
| TC-04.3-02 | FR-04.3 | `TC_04_3_02_SetAssignmentStatus_InvalidStatus_ReturnsError` | [AssessmentServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AssessmentService.Tests/AssessmentServiceTests.cs) |
| TC-04.3-03 | FR-04.3 | `TC_04_3_03_SetAssignmentStatus_AssignmentNotFound_ReturnsNotFound` | [AssessmentServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AssessmentService.Tests/AssessmentServiceTests.cs) |
| TC-04.4-01 | FR-04.4 | `TC_04_4_01_SetExamReminder_StoresDaysBefore` | [AssessmentServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AssessmentService.Tests/AssessmentServiceTests.cs) |
| TC-04.4-02 | FR-04.4 | `TC_04_4_02_SetExamReminder_NegativeDays_ReturnsInvalidReminder` | [AssessmentServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AssessmentService.Tests/AssessmentServiceTests.cs) |
| TC-03.1-01 | FR-03.1 | `TC_03_1_01_AddCourse_MissingRequiredField_ReturnsRequiredFieldError` | [AddCourseTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/CourseService.Tests/AddCourseTests.cs) |
| TC-03.1-02 | FR-03.1 | `TC_03_1_02_AddCourse_DuplicateCodeAndSemester_ReturnsDuplicateError` | [AddCourseTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/CourseService.Tests/AddCourseTests.cs) |
| TC-03.1-03 | FR-03.1 | `TC_03_1_03_AddCourse_SameCodeDifferentSemester_Allowed` | [AddCourseTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/CourseService.Tests/AddCourseTests.cs) |
| TC-03.1-04 | FR-03.1 | `TC_03_1_04_GetCourses_FiltersByStudent` | [GetCoursesTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/CourseService.Tests/GetCoursesTests.cs) |
| TC-03.2-01 | FR-03.2 | `TC_03_2_01_AddCourse_WithValidData_StoresCreditsAndWorkload` | [AddCourseTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/CourseService.Tests/AddCourseTests.cs) |
| TC-03.3-01 | FR-03.3 | `TC_03_3_01_ViewCourseProgress_SeparatesCompletedAndOutstanding` | [ViewCourseProgressTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/CourseService.Tests/ViewCourseProgressTests.cs) |
| TC-03.3-02 | FR-03.3 | `TC_03_3_02_ViewCourseProgress_NoAssignments_ReturnsNoAssignmentsMessage` | [ViewCourseProgressTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/CourseService.Tests/ViewCourseProgressTests.cs) |
| TC-03.3-03 | FR-03.3 | `TC_03_3_03_ViewCourseProgress_CourseNotFound_ReturnsNotFound` | [ViewCourseProgressTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/CourseService.Tests/ViewCourseProgressTests.cs) |
| TC-03.4-01 | FR-03.4 | `TC_03_4_01_UpdateCourse_ChangesFields` | [UpdateCourseTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/CourseService.Tests/UpdateCourseTests.cs) |
| TC-03.5-01 | FR-03.5 | `TC_03_5_01_RemoveCourse_DeletesCourse` | [RemoveCourseTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/CourseService.Tests/RemoveCourseTests.cs) |
| TC-05.1-01 | FR-05.1 | `TC_05_1_01_GeneratePlan_EmptyDescription_ReturnsRequiredFieldError` | [PlansControllerTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/GoalPlannerService.Tests/PlansControllerTests.cs) |
| TC-05.2-01 | FR-05.2 | `TC_05_2_01_GeneratePlan_WithDescription_CreatesDraftWithMilestonesAndTasks` | [PlansControllerTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/GoalPlannerService.Tests/PlansControllerTests.cs) |
| TC-05.2-02 | FR-05.2 | `TC_05_2_02_GeneratePlan_AiUnavailable_ReturnsAiUnavailableError` | [PlansControllerTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/GoalPlannerService.Tests/PlansControllerTests.cs) |
| TC-05.2-03 | FR-05.2 | `TC_05_2_03_GeneratePlan_InvalidStructure_ReturnsPlanInvalidError` | [PlansControllerTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/GoalPlannerService.Tests/PlansControllerTests.cs) |
| TC-05.3-01 | FR-05.3 | `TC_05_3_01_Discard_DeletesDraft` | [PlansControllerTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/GoalPlannerService.Tests/PlansControllerTests.cs) |
| TC-05.3-02 | FR-05.3 | `TC_05_3_02_GetTasks_ReturnsOnlyApprovedPlanTasks` | [PlansControllerTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/GoalPlannerService.Tests/PlansControllerTests.cs) |
| TC-05.3-03 | FR-05.3 | `TC_05_3_03_GetDraft_Missing_ReturnsNotFound` | [PlansControllerTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/GoalPlannerService.Tests/PlansControllerTests.cs) |
| TC-05.3-04 | FR-05.3 | `TC_05_3_04_Discard_Missing_ReturnsNotFound` | [PlansControllerTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/GoalPlannerService.Tests/PlansControllerTests.cs) |
| TC-05.4-01 | FR-05.4 | `TC_05_4_01_UpdateDraft_EditsMilestonesAndTasks` | [PlansControllerTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/GoalPlannerService.Tests/PlansControllerTests.cs) |
| TC-05.5-01 | FR-05.5 | `TC_05_5_01_UpdateDraft_ApprovedPlan_ReturnsNotEditable` | [PlansControllerTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/GoalPlannerService.Tests/PlansControllerTests.cs) |
| TC-05.6-01 | FR-05.6 | `TC_05_6_01_Approve_WithExistingGoal_ApprovesAndSavesOnce` | [GoalPlannerMoqTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/GoalPlannerService.Tests/GoalPlannerMoqTests.cs) |
| TC-05.6-02 | FR-05.6 | `TC_05_6_02_Approve_WhenGoalDoesNotExist_ReturnsGoalNotFoundAndDoesNotSave` | [GoalPlannerMoqTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/GoalPlannerService.Tests/GoalPlannerMoqTests.cs) |
| TC-05.6-03 | FR-05.6 | `TC_05_6_03_Approve_WhenDraftMissing_ReturnsNotFound` | [GoalPlannerMoqTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/GoalPlannerService.Tests/GoalPlannerMoqTests.cs) |
| TC-05.6-04 | FR-05.6 | `TC_05_6_04_Approve_ValidDraft_SetsApproved` | [PlansControllerTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/GoalPlannerService.Tests/PlansControllerTests.cs) |
| TC-05.6-05 | FR-05.6 | `TC_05_6_05_Approve_GoalMissing_ReturnsGoalNotFound` | [PlansControllerTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/GoalPlannerService.Tests/PlansControllerTests.cs) |
| TC-05.6-06 | FR-05.6 | `TC_05_6_06_Approve_AlreadyApproved_IsIdempotent` | [PlansControllerTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/GoalPlannerService.Tests/PlansControllerTests.cs) |
| TC-13.1-01 | FR-13.1 | `TC_13_1_01_CreateHabit_WithValidData_SetsZeroStreak` | [HabitServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/HabitService.Tests/HabitServiceTests.cs) |
| TC-13.1-02 | FR-13.1 | `TC_13_1_02_CreateHabit_MissingName_ReturnsRequiredFieldError` | [HabitServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/HabitService.Tests/HabitServiceTests.cs) |
| TC-13.1-03 | FR-13.1 | `TC_13_1_03_CreateHabit_InvalidFrequency_ReturnsInvalidFrequencyError` | [HabitServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/HabitService.Tests/HabitServiceTests.cs) |
| TC-13.2-01 | FR-13.2 | `TC_13_2_01_CompleteHabit_WhenNoLogExists_AddsLogAndSaves` | [HabitMoqTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/HabitService.Tests/HabitMoqTests.cs) |
| TC-13.2-02 | FR-13.2 | `TC_13_2_02_CompleteHabit_WhenLogAlreadyCompleted_DoesNotAddLog` | [HabitMoqTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/HabitService.Tests/HabitMoqTests.cs) |
| TC-13.2-03 | FR-13.2 | `TC_13_2_03_CompleteHabit_WhenHabitMissing_ReturnsNotFoundAndDoesNotPersist` | [HabitMoqTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/HabitService.Tests/HabitMoqTests.cs) |
| TC-13.3-01 | FR-13.3 | `TC_13_3_01_CompleteHabit_FirstTime_SetsStreakToOne` | [HabitServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/HabitService.Tests/HabitServiceTests.cs) |
| TC-13.3-02 | FR-13.3 | `TC_13_3_02_CompleteHabit_ConsecutiveDays_IncrementsStreak` | [HabitServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/HabitService.Tests/HabitServiceTests.cs) |
| TC-13.3-03 | FR-13.3 | `TC_13_3_03_CompleteHabit_MissedScheduledDay_ResetsStreakToOne` | [HabitServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/HabitService.Tests/HabitServiceTests.cs) |
| TC-13.3-04 | FR-13.3 | `TC_13_3_04_CompleteHabit_SameDayTwice_DoesNotIncrementStreak` | [HabitServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/HabitService.Tests/HabitServiceTests.cs) |
| TC-13.3-05 | FR-13.3 | `TC_13_3_05_GetStreaks_ShowsDoneToday` | [HabitServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/HabitService.Tests/HabitServiceTests.cs) |
| TC-13.4-01 | FR-13.4 | `TC_13_4_01_LinkHabitToGoal_StoresGoalId` | [HabitServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/HabitService.Tests/HabitServiceTests.cs) |
| TC-13.4-02 | FR-13.4 | `TC_13_4_02_LinkHabitToGoal_HabitNotFound_ReturnsNotFound` | [HabitServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/HabitService.Tests/HabitServiceTests.cs) |
| TC-01.1-01 | FR-01.1 | `TC_01_1_01_CreateProfile_WithRequiredFields_CreatesProfile` | [CreateProfileTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/StudentService.Tests/CreateProfileTests.cs) |
| TC-01.1-02 | FR-01.1 | `TC_01_1_02_CreateProfile_Duplicate_ReturnsDuplicateError` | [CreateProfileTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/StudentService.Tests/CreateProfileTests.cs) |
| TC-01.2-01 | FR-01.2 | `TC_01_2_01_UpdateProfile_ChangesFields` | [UpdateProfileTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/StudentService.Tests/UpdateProfileTests.cs) |
| TC-01.2-02 | FR-01.2 | `TC_01_2_02_UpdateProfile_ClearingRequiredField_KeepsOldValues` | [UpdateProfileTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/StudentService.Tests/UpdateProfileTests.cs) |
| TC-01.2-03 | FR-01.2 | `TC_01_2_03_UpdateProfile_ChangesAvailability` | [UpdateProfileTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/StudentService.Tests/UpdateProfileTests.cs) |
| TC-01.2-04 | FR-01.2 | `TC_01_2_04_UpdateProfile_NotFound_ReturnsNotFound` | [UpdateProfileTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/StudentService.Tests/UpdateProfileTests.cs) |
| TC-01.3-01 | FR-01.3 | `TC_01_3_01_ViewProfile_ReturnsAllFields` | [ViewProfileTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/StudentService.Tests/ViewProfileTests.cs) |
| TC-01.3-02 | FR-01.3 | `TC_01_3_02_ViewProfile_OptionalFieldMissing_ShowsNotSet` | [ViewProfileTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/StudentService.Tests/ViewProfileTests.cs) |
| TC-01.3-03 | FR-01.3 | `TC_01_3_03_GetProfile_NotFound_ReturnsNotFound` | [ViewProfileTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/StudentService.Tests/ViewProfileTests.cs) |
| TC-01.3-04 | FR-01.3 | `TC_01_3_04_GetAvailability_ReturnsSlots` | [ViewProfileTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/StudentService.Tests/ViewProfileTests.cs) |
| TC-01.3-05 | FR-01.3 | `TC_01_3_05_GetAvailability_NotFound_ReturnsNotFound` | [ViewProfileTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/StudentService.Tests/ViewProfileTests.cs) |
| TC-01.4-01 | FR-01.4 | `TC_01_4_01_CreateProfile_MissingAcademicLevel_ReturnsRequiredFieldError` | [CreateProfileTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/StudentService.Tests/CreateProfileTests.cs) |
| TC-01.4-02 | FR-01.4 | `TC_01_4_02_CreateProfile_EndBeforeStart_ReturnsInvalidTimeSlot` | [CreateProfileTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/StudentService.Tests/CreateProfileTests.cs) |
| TC-01.4-03 | FR-01.4 | `TC_01_4_03_CreateProfile_MissingMajor_ReturnsRequiredFieldError` | [CreateProfileTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/StudentService.Tests/CreateProfileTests.cs) |

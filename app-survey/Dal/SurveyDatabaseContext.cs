using System.Data.Entity;
using SurveyService.Entities;

namespace SurveyService.Dal
{
    public class SurveyContextConfiguration : DbConfiguration
    {
        public SurveyContextConfiguration()
        {
            SetDatabaseInitializer(new NullDatabaseInitializer<SurveyDatabaseContext>());
        }
    }


    [DbConfigurationType(typeof(SurveyContextConfiguration))]
    public class SurveyDatabaseContext : DbContext
    {
        #region constructors

        public SurveyDatabaseContext()
            : this("name=SurveyService")
        {
        }


        public SurveyDatabaseContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        #endregion

        #region properties

        public virtual DbSet<Answer> Answers { get; set; }


        public virtual DbSet<AnswerOption> AnswerOptions { get; set; }


        public virtual DbSet<Question> Questions { get; set; }


        public virtual DbSet<QuestionOption> QuestionOptions { get; set; }
        public virtual DbSet<QuestionType> QuestionTypes { get; set; }
        public virtual DbSet<Survey> Surveys { get; set; }
        public virtual DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public virtual DbSet<SurveyResponse> SurveyResponses { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Customization of the data model
        /// </summary>
        /// <param name="modelBuilder">Instance of the model builder</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        #endregion
    }
}
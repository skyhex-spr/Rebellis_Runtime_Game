namespace Rebellis
{
    using System;

    [Serializable]
    public class Rebelliprompts
    {
        public string id;
        public int user_id;
        public int worker_model_id;
        public string prompt;
        public int repeat_number;
        public int total_credit_usage;
        public bool generate_fbx;
        public bool is_completed;
        public bool is_active;
        public WorkerModel worker_model;
        public PromptResponse prompt_response;
    }

    [Serializable]
    public class WorkerModel
    {
        public int id;
        public DateTime created_at;
        public DateTime updated_at;
        public string slug;
        public string name;
        public int unit_price;
        public string description;
        public string worker_model_type;
    }

}
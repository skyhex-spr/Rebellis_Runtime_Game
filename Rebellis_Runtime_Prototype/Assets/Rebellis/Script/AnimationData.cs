
namespace Rebellis
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class PromptRequestStep
    {
        public string id;
        public string prompt_request_id;
        public string prompt_response_id;
        public int step_number;
        public string mp4;
        public string fbx;
        public string unity;
    }

    [Serializable]
    public class PromptResponse
    {
        public string created_at;
        public string updated_at;
        public string id;
        public string prompt_request_id;
        public string prompt_response_id;
        public List<PromptRequestStep> prompt_request_step;
    }

    [Serializable]
    public class AnimationData
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
}
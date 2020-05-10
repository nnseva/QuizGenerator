using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

abstract public class NaturalQuizSource : QuizSource {
    // Natural counting quiz source
    public int digits;
    public override string category { get { return GetLocalizedString("Natural numbers"); } }
    public override string theme { get { return GetLocalizedString("Performing {0} of {1}-digits numbers", action_name, digits); } }
    virtual public int a(int id) {
        return (int)(id / Mathf.Pow(10f, (float)digits));
    }
    virtual public int b(int id) {
        return (int)(id % Mathf.Pow(10f, (float)digits));
    }
    public override int random_id() {
        float mn = Mathf.Pow(10f, (float)(digits - 1));
        float mx = Mathf.Pow(10f, (float)(digits));
        int x = (int)Random.Range(mn, mx);
        int y = (int)Random.Range(mn, mx);
        return x * (int) Mathf.Pow(10f, digits) + y;
    }
    public override string question(int id) {
        return GetLocalizedString("How much will {0} {1} {2}?", a(id), action_sign, b(id));
    }
    public override string answer(int id) {
        return GetLocalizedString("{0}", action(a(id), b(id)));
    }
    public override string[] choices(int id) {
        List<string> answers = new List<string>();
        answers.Add(this.answer(id));
        answers.Add(this.answer(random_id()));
        answers.Add(this.answer(random_id()));
        answers.Sort( (string x, string y) => Random.value > 0.5 ? 1:-1 );
        return answers.ToArray();
    }
    abstract public int action(int a, int b);
    abstract public string action_sign { get; } // Action sign: +,-,*,:
    abstract public string action_name { get; } // Action name like "addition"
}

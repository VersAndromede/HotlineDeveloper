﻿using Modules.LevelSelectionSystem;
using Modules.LevelsSystem;
using System.Linq;
using VContainer;

namespace Module.ContinueLevelButtonSystem
{
    public class ContinueLevelButtonPresenter
    {
        [Inject]
        internal ContinueLevelButtonPresenter(
            LevelsData levels,
            ContinueLevelButton continueLevelButton, 
            ContinueLevelButtonView view, 
            LevelSceneLoader levelSceneLoader)
        {
            continueLevelButton.Init(levels, levelSceneLoader);

            if (levels.Value[0].IsCompleted)
                view.SetContinueText();
            else
                view.SetStartText();
        }
    }
}

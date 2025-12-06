using System.Collections;
using System.Collections.Generic;
using AlternativeGamesTest.Service;
using UnityEngine;

public class ConfigService
{
    private PlayersIconConfig _playersIconConfig;

    public PlayersIconConfig PlayersIconConfig => _playersIconConfig;
    
    public ConfigService(PlayersIconConfig iconConfig)
    {
        _playersIconConfig = iconConfig;
    }
}

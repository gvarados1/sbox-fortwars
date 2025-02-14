﻿// Copyright (c) 2022 Ape Tavern, do not share, re-distribute or modify
// without permission of its author (insert_email_here)

using Sandbox;
using System.Linq;

namespace Fortwars;

public class LobbyRound : BaseRound
{
	public override string RoundName => "Lobby";
	public override int RoundDuration => 60;

	protected override void OnStart()
	{
		Log.Info( "Started Lobby Round" );

		if ( Host.IsServer )
		{
			Entity.All.OfType<FortwarsPlayer>().ToList().ForEach( ( player ) =>  player ?.Respawn() );
		}
	}

	protected override void OnFinish()
	{
		Log.Info( "Finished Lobby Round" );
	}

	protected override void OnTimeUp()
	{
		if ( Client.All.Count >= Game.Instance.MinPlayers )
			Game.Instance.ChangeRound( new BuildRound() );
		else
			Game.Instance.ChangeRound( new LobbyRound() );
	}

	public override void OnPlayerKilled( Player player )
	{
		player.Respawn();

		base.OnPlayerKilled( player );
	}

	public override void OnPlayerSpawn( Player player )
	{
		base.OnPlayerSpawn( player );
	}
}

// Copyright Epic Games, Inc. All Rights Reserved.

#pragma once

#include "CoreMinimal.h"
#include "GameFramework/GameModeBase.h"
#include "AMIRAGameModeBase.generated.h"

/**
 * 
 */
UCLASS()
class AMIRA_API AAMIRAGameModeBase : public AGameModeBase
{
	GENERATED_BODY()

public:
    virtual void EndPlay(const EEndPlayReason::Type EndPlayReason) override;
    virtual void BeginPlay() override;

    // Function to save actors' transforms to a file
        void SaveActorTransforms(const TArray<AActor*>&ActorsToSave);

    // Function to load actors' transforms from a file and apply them
    void LoadActorTransforms(TArray<AActor*>& ActorsToLoad);
	
};

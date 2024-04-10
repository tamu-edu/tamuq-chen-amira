// Copyright Epic Games, Inc. All Rights Reserved.


#include "AMIRAGameModeBase.h"
#include "Kismet/GameplayStatics.h"
#include "Misc/FileHelper.h"
#include "Misc/Paths.h"

void AAMIRAGameModeBase::EndPlay(const EEndPlayReason::Type EndPlayReason)
{
    Super::EndPlay(EndPlayReason);

    // Gather all actors to save
    TArray<AActor*> ActorsToSave;
    UGameplayStatics::GetAllActorsOfClass(GetWorld(), AActor::StaticClass(), ActorsToSave);

    SaveActorTransforms(ActorsToSave);
}

void AAMIRAGameModeBase::BeginPlay()
{
    Super::BeginPlay();

    // Assuming you have a method to find/load the actors you want to set transforms for
    TArray<AActor*> ActorsToLoad;
    UGameplayStatics::GetAllActorsOfClass(GetWorld(), AActor::StaticClass(), ActorsToLoad);

    LoadActorTransforms(ActorsToLoad);
}

void AAMIRAGameModeBase::SaveActorTransforms(const TArray<AActor*>& ActorsToSave)
{
    FString SaveData;
    for (AActor* Actor : ActorsToSave)
    {
        if (Actor)
        {
            // Convert each transform to a string and add it to the save data
            FTransform ActorTransform = Actor->GetActorTransform();
            SaveData += Actor->GetName() + " " + ActorTransform.ToString() + LINE_TERMINATOR;
        }
    }

    // Save the string to a file
    FString SaveDirectory = FPaths::ProjectSavedDir();
    FString FileName = FString(TEXT("ActorTransformsSave.txt"));
    FString FullPath = SaveDirectory + FileName;

    FFileHelper::SaveStringToFile(SaveData, *FullPath);
}

void AAMIRAGameModeBase::LoadActorTransforms(TArray<AActor*>& ActorsToLoad)
{
    FString LoadDirectory = FPaths::ProjectSavedDir();
    FString FileName = FString(TEXT("ActorTransformsSave.txt"));
    FString FullPath = LoadDirectory + FileName;

    FString LoadedData;
    FFileHelper::LoadFileToString(LoadedData, *FullPath);

    TArray<FString> Lines;
    LoadedData.ParseIntoArrayLines(Lines);

    for (FString& Line : Lines)
    {
        FString ActorName;
        FString TransformString;

        // Assuming the actor's name is first, followed by the transform
        Line.Split(" ", &ActorName, &TransformString);

        for (AActor* Actor : ActorsToLoad)
        {
            if (Actor && Actor->GetName() == ActorName)
            {
                FTransform ActorTransform;
                ActorTransform.InitFromString(TransformString);
                Actor->SetActorTransform(ActorTransform);
                break;
            }
        }
    }
}


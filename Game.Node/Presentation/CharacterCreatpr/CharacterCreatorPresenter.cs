public partial class CharacterCreatorPresenter()
{
	public CharacterCreatorViewModel OnViewReady()
	{
		var res = new GetStartCharactersUseCase().Execute(new());
		var data = res.Value;
		return new(Data: data);
	}
}

export class User {
  public Id: string;
  public Name: string;
  public Email: string;
  public Role: string;
}

export class Watcher extends User {
  public IdeaId: string;
}

export class Alert extends User {
  public AlertId: string;
  public IdeaId: string;
  public IdeaName: string;
  public AlertType: string;
  public AlertDescription: string;
  public CreatedDate: string;
  public AlertFlag: boolean;
}

export class Dashboard {
  public TotalIdeas: string;
  public IdeasInAction: string;
  public IdeasPendingAction: string;
  public IdeasTrending: string;
  public MostCommented: Idea;
}

export class Comment extends User {
  public CommentId: string;
  public IdeaId: string;
  public ParentCommentId: string;
  public CommentDescription: string;
  public CreatedDate: string;
  public UpdatedDate: string;
}

export class Idea extends User {
  public IdeaId: string;
  public Type: string;
  public IdeaName: string;
  public Title: string;
  public Source: string;
  public Description: string;
  public BusinessCase: string;
  public IdealTime: string;
  public BusinessJustification: string;
  public ContactName: string;
  public ContactEmail: string;
  public ContactMobileNo: string;
  public IdeaStatus: string;
  public CreatedDate: string;
  public UpdatedDate: string;
  public WatchCount: number;
  public CommentCount: number;
  public Moderator: string;
  public Picker: string;
}
